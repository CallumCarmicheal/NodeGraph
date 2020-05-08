using NodeGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGraph.History
{
    public class CreateConnectorCommand : NodeGraphCommand
    {
        #region Constructor

        public CreateConnectorCommand( NodeGraphManager ngm, string name, object undoParams, object redoParams )
            : base(ngm, name, undoParams, redoParams)
        {

        }

        #endregion // Constructor

        #region Overrides NodeGraphCommand

        public override void Undo()
        {
            Guid guid = (Guid)UndoParams;

            NodeGraphManager.DestroyConnector(guid);
        }

        public override void Redo()
        {
            NodeGraphManager.DeserializeConnector(RedoParams as string);
        }

        #endregion // Overrides NodeGraphCommand
    }
}
