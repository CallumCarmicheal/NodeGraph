using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NodeGraph.View.ViewModels {
    //private class Temp : UserControl {
    //    public bool IsSelected {
    //        get { return (bool)GetValue(IsSelectedProperty); }
    //        set { SetValue(IsSelectedProperty, value); }
    //    }

    //    public static readonly DependencyProperty IsSelectedProperty =
    //        DependencyProperty.Register("IsSelected", typeof(bool), typeof(NodeView), new PropertyMetadata(false));

    //    public bool HasConnection {
    //        get { return (bool)GetValue(HasConnectionProperty); }
    //        set { SetValue(HasConnectionProperty, value); }
    //    }

    //    public static readonly DependencyProperty HasConnectionProperty =
    //        DependencyProperty.Register("HasConnection", typeof(bool), typeof(NodeView), new PropertyMetadata(false));

    //    public Thickness SelectionThickness {
    //        get { return (Thickness)GetValue(SelectionThicknessProperty); }
    //        set { SetValue(SelectionThicknessProperty, value); }
    //    }

    //    public static readonly DependencyProperty SelectionThicknessProperty =
    //        DependencyProperty.Register("SelectionThickness", typeof(Thickness), typeof(NodeView), new PropertyMetadata(new Thickness(2.0)));

    //    public double CornerRadius {
    //        get { return (double)GetValue(CornerRadiusProperty); }
    //        set { SetValue(CornerRadiusProperty, value); }
    //    }
    //    public static readonly DependencyProperty CornerRadiusProperty =
    //        DependencyProperty.Register("CornerRadius", typeof(double), typeof(NodeView), new PropertyMetadata(8.0));

    //    public BitmapImage ExecutionStateImage {
    //        get { return (BitmapImage)GetValue(ExecutionStateImageProperty); }
    //        set { SetValue(ExecutionStateImageProperty, value); }
    //    }
    //    public static readonly DependencyProperty ExecutionStateImageProperty =
    //        DependencyProperty.Register("ExecutionStateImage", typeof(BitmapImage), typeof(NodeView),
    //            new PropertyMetadata(null));
    //}

    //public class NodeViewViewModel : ViewModelBase {
    //    protected bool _IsSelected;
    //    public bool IsSelected {
    //        get { return _IsSelected; }
    //        set {
    //            if (value != null || value != _IsSelected) _IsSelected = value;
    //            OnPropertyChanged("IsSelected");
    //        }
    //    }


    //    protected bool _HasConnection;
    //    public bool HasConnection {
    //        get { return _HasConnection; }
    //        set {
    //            if (value != null || value != _HasConnection) _HasConnection = value;
    //            OnPropertyChanged("HasConnection");
    //        }
    //    }


    //    protected Thickness _SelectionThickness;
    //    public Thickness SelectionThickness {
    //        get { return _SelectionThickness; }
    //        set {
    //            if (value != null || value != _SelectionThickness) _SelectionThickness = value;
    //            OnPropertyChanged("SelectionThickness");
    //        }
    //    }


    //    protected double _CornerRadius;
    //    public double CornerRadius {
    //        get { return _CornerRadius; }
    //        set {
    //            if (value != null || value != _CornerRadius) _CornerRadius = value;
    //            OnPropertyChanged("CornerRadius");
    //        }
    //    }


    //    protected BitmapImage _ExecutionStateImage;
    //    public BitmapImage ExecutionStateImage {
    //        get { return _ExecutionStateImage; }
    //        set {
    //            if (value != null || value != _ExecutionStateImage) _ExecutionStateImage = value;
    //            OnPropertyChanged("ExecutionStateImage");
    //        }
    //    }
    //}
}
