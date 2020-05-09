using NodeGraph.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.ViewModel {
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeViewModelAttribute : Attribute {
        public string ViewStyleName { get; set; } = "DefaultNodeViewStyle";
        public Type ViewType { get; set; } = typeof(NodeView);
    }
}

