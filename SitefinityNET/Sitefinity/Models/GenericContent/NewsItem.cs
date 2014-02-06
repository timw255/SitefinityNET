using Newtonsoft.Json;
using SitefinityNET.JSON;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Models.GenericContent
{
    public class NewsItem : Content, IPersistableContentItem
    {
        [JsonConverter(typeof(LstringConverter))]
        public string ApprovalWorkflowState { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Author { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Content { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ItemDefaultUrl { get; set; }

        public string SourceName { get; set; }

        public string SourceSite { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Summary { get; set; }
    }
}