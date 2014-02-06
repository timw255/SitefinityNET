using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Types
{
    public class WcfChange
    {
        public string ChangeDescription { get; set; }

        public string ChangeType { get; set; }

        public string Comment { get; set; }

        public string CreatedByUserName { get; set; }

        public Guid Id { get; set; }

        public bool IsLastPublishedVersion { get; set; }

        public bool IsPublishedVersion { get; set; }

        public Guid ItemId { get; set; }

        public string Label { get; set; }

        public DateTime LastModified { get; set; }

        public string NextId { get; set; }

        public int NextVersionNumber { get; set; }

        public Guid Owner { get; set; }

        public string PreviousId { get; set; }

        public int PrevVersionNumber { get; set; }

        public string Version { get; set; }

        public int VersionNumber { get; set; }
    }
}