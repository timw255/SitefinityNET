using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Types
{
    public class VersionInfo
    {
        public string ChangeDescription { get; set; }

        public string ChangeType { get; set; }

        public string Comment { get; set; }

        public string CreatedByUserName { get; set; }

        public string Id { get; set; }

        public bool IsLastPublishedVersion { get; set; }

        public bool IsPublishedVersion { get; set; }

        public string ItemId { get; set; }

        public string Label { get; set; }

        public DateTime LastModified { get; set; }

        public string NextId { get; set; }

        public long NextVersionNumber { get; set; }

        public string Owner { get; set; }

        public long PrevVersionNumber { get; set; }

        public string PreviousId { get; set; }

        public string Version { get; set; }

        public long VersionNumber { get; set; }
    }
}