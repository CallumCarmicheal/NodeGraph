using NodeGraph.Model;
using NodeGraph.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NodeGraph.ViewModel {
    [NodeViewModel()]
    public class NodeViewModel : ViewModelBase {
        #region Fields

        public NodeView View { get; set; }

        public readonly Type NodeViewType;

        #endregion // Fields

        #region Properties

        public NodeGraphManager NodeGraphManager { get; internal set; }

        private Node _Model;
        public Node Model {
            get { return _Model; }
            set {
                if (value != _Model) {
                    _Model = value;
                    RaisePropertyChanged("Model");
                }
            }
        }

        public Visibility InputFlowPortsVisibility {
            get { return (0 < _InputFlowPortViewModels.Count) ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility OutputFlowPortsVisibility {
            get { return (0 < _OutputFlowPortViewModels.Count) ? Visibility.Visible : Visibility.Collapsed; }
        }

        private ObservableCollection<NodeFlowPortViewModel> _InputFlowPortViewModels = new ObservableCollection<NodeFlowPortViewModel>();
        public ObservableCollection<NodeFlowPortViewModel> InputFlowPortViewModels {
            get { return _InputFlowPortViewModels; }
            set {
                if (value != _InputFlowPortViewModels) {
                    _InputFlowPortViewModels = value;
                    RaisePropertyChanged("InputFlowPortViewModels");
                }
            }
        }

        private ObservableCollection<NodeFlowPortViewModel> _OutputFlowPortViewModels = new ObservableCollection<NodeFlowPortViewModel>();
        public ObservableCollection<NodeFlowPortViewModel> OutputFlowPortViewModels {
            get { return _OutputFlowPortViewModels; }
            set {
                if (value != _OutputFlowPortViewModels) {
                    _OutputFlowPortViewModels = value;
                    RaisePropertyChanged("OutputFlowPortViewModels");
                }
            }
        }

        private bool _IsSelected = false;
        public bool IsSelected {
            get { return _IsSelected; }
            set {
                if (value != _IsSelected) {
                    _IsSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        #endregion // Node Properties

        #region NodePropertyPorts

        public Visibility InputPropertyPortsVisibility {
            get { return (0 < _InputPropertyPortViewModels.Count) ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility OutputPropertyPortsVisibility {
            get { return (0 < _OutputPropertyPortViewModels.Count) ? Visibility.Visible : Visibility.Collapsed; }
        }

        private ObservableCollection<NodePropertyPortViewModel> _InputPropertyPortViewModels = new ObservableCollection<NodePropertyPortViewModel>();
        public ObservableCollection<NodePropertyPortViewModel> InputPropertyPortViewModels {
            get { return _InputPropertyPortViewModels; }
            set {
                if (value != _InputPropertyPortViewModels) {
                    _InputPropertyPortViewModels = value;
                    RaisePropertyChanged("InputPropertyPortViewModels");
                }
            }
        }

        private ObservableCollection<NodePropertyPortViewModel> _OutputPropertyPortViewModels = new ObservableCollection<NodePropertyPortViewModel>();
        public ObservableCollection<NodePropertyPortViewModel> OutputPropertyPortViewModels {
            get { return _OutputPropertyPortViewModels; }
            set {
                if (value != _OutputPropertyPortViewModels) {
                    _OutputPropertyPortViewModels = value;
                    RaisePropertyChanged("OutputPropertyPortViewModels");
                }
            }
        }

        #endregion // NodePropertyPorts

        #region Constructors

        public NodeViewModel(Node node) : base(node) {
            Model = node ?? throw new ArgumentException("Node can not be null in NodeViewModel constructor");
        }

        #endregion // Constructors

        #region Events

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.ModelPropertyChanged(sender, e);

            RaisePropertyChanged(e.PropertyName);
        }

        #endregion // Events


        #region DependancyProperty Extracted

        protected bool _HasConnection = false;
        public bool HasConnection {
            get { return _HasConnection; }
            set {
                if (value != _HasConnection) _HasConnection = value;
                RaisePropertyChanged("HasConnection");
            }
        }


        protected Thickness _SelectionThickness = new Thickness(2.0);
        public Thickness SelectionThickness {
            get { return _SelectionThickness; }
            set {
                if (value != null || value != _SelectionThickness) _SelectionThickness = value;
                RaisePropertyChanged("SelectionThickness");
            }
        }


        protected CornerRadius _CornerRadius = new CornerRadius(8, 8, 8, 8);
        public CornerRadius CornerRadius {
            get { return _CornerRadius; }
            set {
                if (value != _CornerRadius) 
                    _CornerRadius = value;

                RaisePropertyChanged("CornerRadius");
            }
        }

        CornerRadius _topHeaderCornerRadius = new CornerRadius(8, 8, 0, 0);
        public CornerRadius TopHeaderCornerRadius {
            get { return _topHeaderCornerRadius; }
            set {
                if (value != _topHeaderCornerRadius)
                    _topHeaderCornerRadius = value;
                RaisePropertyChanged("TopHeaderCornerRadius");
            }
        }


        protected BitmapImage _ExecutionStateImage = null;
        public BitmapImage ExecutionStateImage {
            get { return _ExecutionStateImage; }
            set {
                if (value != null || value != _ExecutionStateImage) _ExecutionStateImage = value;
                RaisePropertyChanged("ExecutionStateImage");
            }
        }
        #endregion


    }
}
