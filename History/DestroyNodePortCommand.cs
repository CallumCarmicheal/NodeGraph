using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.History
{
    public class DestroyNodePortCommand : NodeGraphCommand
    {
        #region Constructor

        public DestroyNodePortCommand( NodeGraphManager ngm, string name, object undoParams, object redoParams ) : base(ngm, name, undoParams, redoParams)
        {

        }

        #endregion // Constructor


        #region Overrides NodeGraphCommand

        public override void Undo()
        {
            NodeGraphManager.DeserializeNodePort(UndoParams as string);
        }

        public override void Redo()
        {
            Guid guid = (Guid)RedoParams;

            NodeGraphManager.DestroyNodePort(guid);
        }

        #endregion // Overrides NodeGraphCommand
    }
}
