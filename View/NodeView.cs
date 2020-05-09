using NodeGraph.Model;
using NodeGraph.ViewModel;
using PropertyTools.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace NodeGraph.View {
    [TemplatePart(Name = "PART_Header", Type = typeof(EditableTextBlock))]
    public class NodeView : ContentControl {
        #region Fields

        private EditableTextBlock _Part_Header;
        private DispatcherTimer _ClickTimer = new DispatcherTimer();
        private int _ClickCount = 0;

        private BitmapImage[] _ExecutionStateImages;

        #endregion // Fields

        #region Properties

        private NodeViewModel _viewModel = null;
        public NodeViewModel ViewModel {
            get => _viewModel;
            private set {
                _viewModel = value;
            }
        }

        public NodeGraphManager NodeGraphManager { get; internal set; }


        // View properties

        public Thickness SelectionThickness { get => ViewModel.SelectionThickness; set => ViewModel.SelectionThickness = value; }
        public double CornerRadius { get => ViewModel.CornerRadius; set => ViewModel.CornerRadius = value; }
        public double CornerRadiusProperty { get => CornerRadius; set => CornerRadius = value; }
        #endregion // Properties

        #region Constructors

        private BitmapImage LoadBitmapImage(Uri uri) {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = uri;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            return image;
        }


        public NodeView() {
            DataContextChanged += NodeView_DataContextChanged;
            Loaded += NodeView_Loaded;
            Unloaded += NodeView_Unloaded;

            _ExecutionStateImages = new BitmapImage[4];

            _ExecutionStateImages[0] = new BitmapImage();

            _ExecutionStateImages[1] = LoadBitmapImage(
                new Uri("pack://application:,,,/NodeGraph;component/Resources/Images/Executing.png"));

            _ExecutionStateImages[2] = LoadBitmapImage(
                new Uri("pack://application:,,,/NodeGraph;component/Resources/Images/Executed.png"));

            _ExecutionStateImages[3] = LoadBitmapImage(
                new Uri("pack://application:,,,/NodeGraph;component/Resources/Images/Failed.png"));


        }

        #endregion // Constructors

        #region Events

        private void NodeView_Loaded(object sender, RoutedEventArgs e) {
            SynchronizeProperties();
            OnCanvasRenderTransformChanged();

            _ClickTimer.Interval = TimeSpan.FromMilliseconds(300);
            _ClickTimer.Tick += _ClickTimer_Tick;
        }

        private void _ClickTimer_Tick(object sender, EventArgs e) {
            _ClickCount = 0;
            _ClickTimer.Stop();
        }

        private void NodeView_Unloaded(object sender, RoutedEventArgs e) {

        }

        private void NodeView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            ViewModel = DataContext as NodeViewModel;
            if (null == ViewModel)
                throw new Exception("ViewModel must be bound as DataContext in NodeView.");
            ViewModel.View = this;
            ViewModel.PropertyChanged += ViewModelPropertyChanged;

            SynchronizeProperties();
        }

        protected virtual void SynchronizeProperties() {
            if (null == ViewModel) {
                return;
            }

            //IsSelected = ViewModel.IsSelected;

            ViewModel.PropertyChanged -= ViewModelPropertyChanged;

            ViewModel.HasConnection = (0 < ViewModel.InputFlowPortViewModels.Count) ||
                (0 < ViewModel.OutputFlowPortViewModels.Count) ||
                (0 < ViewModel.InputPropertyPortViewModels.Count) ||
                (0 < ViewModel.OutputPropertyPortViewModels.Count);

            ViewModel.ExecutionStateImage = _ExecutionStateImages[(int)ViewModel.Model.ExecutionState];

            ViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        protected virtual void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            SynchronizeProperties();
        }

        #endregion // Events

        #region Template Events

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            _Part_Header = Template.FindName("PART_Header", this) as EditableTextBlock;
            if (null != _Part_Header) {
                _Part_Header.MouseDown += _Part_Header_MouseDown;
            }
        }

        #endregion // Template Events

        #region Header Events

        private void _Part_Header_MouseDown(object sender, MouseButtonEventArgs e) {
            Keyboard.Focus(_Part_Header);

            if (0 == _ClickCount) {
                _ClickTimer.Start();
                _ClickCount++;
            } else if (1 == _ClickCount) {
                _Part_Header.IsEditing = true;
                Keyboard.Focus(_Part_Header);
                _ClickCount = 0;
                _ClickTimer.Stop();

                e.Handled = true;
            }
        }

        #endregion // Header Events

        #region Mouse Events

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonUp(e);

            FlowChart flowChart = ViewModel.Model.Owner;

            if (NodeGraphManager.IsConnecting) {
                bool bConnected;
                flowChart.History.BeginTransaction("Creating Connection");
                {
                    bConnected = NodeGraphManager.EndConnection();
                }
                flowChart.History.EndTransaction(!bConnected);
            }

            if (NodeGraphManager.IsSelecting) {
                bool bChanged = false;
                flowChart.History.BeginTransaction("Selecting");
                {
                    bChanged = NodeGraphManager.EndDragSelection(false);
                }
                flowChart.History.EndTransaction(!bChanged);
            }

            if (!NodeGraphManager.AreNodesReallyDragged &&
                NodeGraphManager.MouseLeftDownNode == ViewModel.Model) {
                flowChart.History.BeginTransaction("Selection");
                {
                    NodeGraphManager.TrySelection(flowChart, ViewModel.Model,
                        Keyboard.IsKeyDown(Key.LeftCtrl),
                        Keyboard.IsKeyDown(Key.LeftShift),
                        Keyboard.IsKeyDown(Key.LeftAlt));
                }
                flowChart.History.EndTransaction(false);
            }

            NodeGraphManager.EndDragNode();

            NodeGraphManager.MouseLeftDownNode = null;

            e.Handled = true;
        }

        private Point _DraggingStartPos;
        private Matrix _ZoomAndPanStartMatrix;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);

            FlowChart flowChart = ViewModel.Model.Owner;
            FlowChartView flowChartView = flowChart.ViewModel.View;
            Keyboard.Focus(flowChartView);

            NodeGraphManager.EndConnection();
            NodeGraphManager.EndDragNode();
            NodeGraphManager.EndDragSelection(false);

            NodeGraphManager.MouseLeftDownNode = ViewModel.Model;

            NodeGraphManager.BeginDragNode(flowChart);

            Node node = ViewModel.Model;
            _DraggingStartPos = new Point(node.X, node.Y);

            flowChart.History.BeginTransaction("Moving node");

            _ZoomAndPanStartMatrix = flowChartView.ZoomAndPan.Matrix;

            e.Handled = true;
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e) {
            base.OnPreviewMouseLeftButtonUp(e);

            if (NodeGraphManager.IsNodeDragging) {
                FlowChart flowChart = ViewModel.Model.Owner;

                Node node = ViewModel.Model;
                Point delta = new Point(node.X - _DraggingStartPos.X, node.Y - _DraggingStartPos.Y);

                if ((0 != (int)delta.X) &&
                    (0 != (int)delta.Y)) {
                    ObservableCollection<Guid> selectionList = NodeGraphManager.GetSelectionList(node.Owner);
                    foreach (var guid in selectionList) {
                        Node currentNode = NodeGraphManager.FindNode(guid);

                        flowChart.History.AddCommand(new History.NodePropertyCommand(NodeGraphManager,
                            "Node.X", currentNode.Guid, "X", currentNode.X - delta.X, currentNode.X));

                        flowChart.History.AddCommand(new History.NodePropertyCommand(NodeGraphManager,
                            "Node.Y", currentNode.Guid, "Y", currentNode.Y - delta.Y, currentNode.Y));
                    }

                    flowChart.History.AddCommand(new History.ZoomAndPanCommand(NodeGraphManager,
                        "ZoomAndPan", flowChart, _ZoomAndPanStartMatrix, flowChart.ViewModel.View.ZoomAndPan.Matrix));

                    flowChart.History.EndTransaction(false);
                } else {
                    flowChart.History.EndTransaction(true);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (NodeGraphManager.IsNodeDragging && (NodeGraphManager.MouseLeftDownNode == ViewModel.Model) &&
                !ViewModel.IsSelected) {
                Node node = ViewModel.Model;
                FlowChart flowChart = node.Owner;
                NodeGraphManager.TrySelection(flowChart, node, false, false, false);
            }
        }

        #endregion // Mouse Events

        #region RenderTrasnform

        public void OnCanvasRenderTransformChanged() {
            if (VisualParent != null) {
                Matrix matrix = (VisualParent as Canvas).RenderTransform.Value;
                double scale = matrix.M11;

                SelectionThickness = new Thickness(2.0 / scale);
                CornerRadius = 8.0 / scale;
            } else {
#if(DEBUG)
                // This happens when ever you recreate a nodegraphmanager on a view that is already initailized
                //
                //MessageBox.Show("NodeGraph failed to render transformed\n" +
                //    "Path: NodeGraph.View.NodeView.cs @ OnCanvasRenderTransformChanged" +
                //    "Reason: VisualParent == null");
#endif
            }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent) {
            base.OnVisualParentChanged(oldParent);

            if (VisualParent != null) {
                OnCanvasRenderTransformChanged();
            }
        }

        #endregion // RenderTransform
    }

    
}
