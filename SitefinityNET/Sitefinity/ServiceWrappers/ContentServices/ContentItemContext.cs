using SitefinityNET.Sitefinity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.ServiceWrappers.ContentServices
{
    public class ContentItemContext<T>
        where T: new()
    {
        public string[] AdditionalUrlNames { get; set; }

        public bool AdditionalUrlsRedirectToDefault { get; set; }

        public bool AllowMultipleUrls { get; set; }

        public string DefaultUrl { get; set; }

        public T Item { get; set; }

        public string ItemType { get; set; }

        public WcfApprovalTrackingRecord LastApprovalTrackingRecord { get; set; }

        public WcfContentLifecycleStatus LifecycleStatus { get; set; }

        public List<WcfPipeSettings> PublicationSettings { get; set; }

        public WcfChange VersionInfo { get; set; }

        public ContentItemContext()
        {
            this.Item = new T();
        }

        public ContentItemContext(T item)
        {
            this.Item = item;
            this.AdditionalUrlNames = new string[] { };
            this.DefaultUrl = "";
        }
    }
}
