using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Types
{
    public class WcfApprovalTrackingRecord
    {
        public DateTime DateCreated { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        public string UIStatus { get; set; }

        public string UserName { get; set; }
    }
}