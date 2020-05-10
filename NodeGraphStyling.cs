using NodeGraph.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeGraph {
    public class NodeGraphStyling : ViewModelSimpleBase {

        protected CornerRadius _NodeCornerRadius = new CornerRadius(8,8,8,8);
        public CornerRadius NodeCornerRadius {
            get { return _NodeCornerRadius; }
            set {
                if (value != null || value != _NodeCornerRadius) _NodeCornerRadius = value;
                OnPropertyChanged();
            }
        }

        protected CornerRadius _NodeHeaderCornerRadius = new CornerRadius(8, 8, 0, 0);
        public CornerRadius NodeHeaderCornerRadius {
            get { return _NodeHeaderCornerRadius; }
            set {
                if (value != null || value != _NodeHeaderCornerRadius) _NodeHeaderCornerRadius = value;
                OnPropertyChanged();
            }
        }
    }
}
