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
    public class Image : Content, IPersistableContentItem
    {
        public Album Album { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string AlternativeText { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ApprovalWorkflowState { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Author { get; set; }
        
        public string BlobStorageProvider { get; set; }

        public string[] Category { get; set; }
        
        public string Extension { get; set; }

        public Guid? FolderId { get; set; }

        public int Height { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ItemDefaultUrl { get; set; }
        
        public string MediaUrl { get; set; }
        
        public string[] Tags { get; set; }

        public string[] ThumbnailNames { get; set; }

        public string ThumbnailUrl { get; set; }

        public int TotalSize { get; set; }
        
        public int Width { get; set; }

        public Image()
        {
            this.AlternativeText = "";
            this.ApprovalWorkflowState = "";
            this.Category = new string[] { };
            this.Tags = new string[] { };
            this.ThumbnailNames = new string[] { };
            this.Author = "";
            this.ItemDefaultUrl = "";
            this.Extension = "";
            this.DefaultPageId = null;
            this.MediaUrl = "";
            this.ThumbnailUrl = "";
            this.PostRights = Types.PostRights.None;
            this.LastModified = DateTime.UtcNow;
            this.PublicationDate = DateTime.UtcNow;
        }
    }
}