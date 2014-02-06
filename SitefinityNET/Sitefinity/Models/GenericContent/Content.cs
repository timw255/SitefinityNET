using Newtonsoft.Json;
using SitefinityNET.JSON;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using SitefinityNET.Sitefinity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Models.GenericContent
{
    public abstract class Content : CustomizableFields
    {
        public bool AllowComments { get; set; }

        public bool? AllowTrackBacks { get; set; }

        public bool ApproveComments { get; set; }

        public string[] AvailableLanguages { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid? DefaultPageId { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Description { get; set; }

        public bool EmailAuthor { get; set; }

        public virtual DateTime? ExpirationDate { get; set; }

        public Guid Id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastModified { get; set; }

        public Guid? LastModifiedBy { get; set; }

        public Guid? OriginalContentId { get; set; }

        public virtual Guid Owner { get; set; }

        public PostRights PostRights { get; set; }

        public DateTime PublicationDate { get; set; }

        public ContentLifecycleStatus Status { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Title { get; set; }

        public ContentUIStatus UIStatus { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string UrlName { get; set; }

        public int Version { get; set; }

        public int ViewsCount { get; set; }

        public bool Visible { get; set; }

        public uint VotesCount { get; set; }

        public decimal VotesSum { get; set; }

        public Content()
        {
            this.AllowComments = true;
            this.AvailableLanguages = new string[]{};
            this.DefaultPageId = Guid.Empty;
            this.Id = Guid.Empty;
            this.LastModifiedBy = Guid.Empty;
            this.OriginalContentId = Guid.Empty;
            this.DateCreated = DateTime.UtcNow;
            this.LastModified = DateTime.UtcNow;
            this.Description = "";
        }
    }
}