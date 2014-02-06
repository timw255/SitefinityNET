using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Types
{
    public class WorkflowOperation
    {
        public string ArgumentDialogName { get; set; }

        public bool ClosesForm { get; set; }

        public string ContentCommandName { get; set; }

        public string CssClass { get; set; }

        public string DecisionType { get; set; }

        public string OperationName { get; set; }

        public long Ordinal { get; set; }

        public bool PersistOnDecision { get; set; }

        public bool RunAsUICommand { get; set; }

        public string Title { get; set; }

        public int VisualType { get; set; }

        public string WarningMessage { get; set; }
    }
}