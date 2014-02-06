using Newtonsoft.Json;
using SitefinityNET.JSON;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Models.GenericContent
{
    public class Album : Content, IPersistableContentItem
    {
        public string BlobStorageProvider { get; set; }

        public int ClientCacheDuration { get; set; }

        public string ClientCacheProfile { get; set; }

        public string DownloadSecurityProviderName { get; set; }

        public bool EnableClientCache { get; set; }

        public bool EnableOutputCache { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ItemDefaultUrl { get; set; }

        public int MaxItemSize { get; set; }

        public int MaxSize { get; set; }

        public int NewSize { get; set; }

        public int OutputCacheDuration { get; set; }

        public int OutputCacheMaxSize { get; set; }

        public string OutputCacheProfile { get; set; }

        public bool OutputSlidingExpiration { get; set; }
        
        public Guid? ParentId { get; set; }
        
        public bool ResizeOnUpload { get; set; }

        public Guid? RunningTask { get; set; }
        
        public List<string> ThumbnailProfiles { get; set; }
        
        public bool UseDefaultSettingsForClientCaching { get; set; }

        public bool UseDefaultSettingsForOutputCaching { get; set; }

        public Album()
        {
            this.ParentId = Guid.Empty;
            this.RunningTask = Guid.Empty;
        }
    }
}