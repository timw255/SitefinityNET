using Newtonsoft.Json;
using SitefinityNET.JSON;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using SitefinityNET.Sitefinity.Types;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Models.GenericContent
{
    public class Blog : Content, IPersistableContentItem
    {
        [JsonConverter(typeof(LstringConverter))]
        public string ItemDefaultUrl { get; set; }

        public Guid LandingPageId { get; set; }

        public Blog()
        {
            this.LandingPageId = Guid.Empty;
        }
    }
}