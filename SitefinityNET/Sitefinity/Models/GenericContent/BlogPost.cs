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
    public class BlogPost : Content, IPersistableContentItem
    {
        [JsonConverter(typeof(LstringConverter))]
        public string ApprovalWorkflowState { get; set; }

        public string[] Category { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Content { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ItemDefaultUrl { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Summary { get; set; }

        public string[] Tags { get; set; }

        public BlogPost()
        {
            this.ApprovalWorkflowState = "";
            this.Category = new string[] { };
            this.Content = "";
            this.ItemDefaultUrl = "";
            this.Summary = "";
            this.Tags = new string[] { };
        }
    }
}