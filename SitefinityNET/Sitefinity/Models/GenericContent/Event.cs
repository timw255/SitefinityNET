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
    public class Event : Content, IPersistableContentItem
    {
        public bool AllDayEvent { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ApprovalWorkflowState { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string City { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ContactCell { get; set; }

        public string ContactEmail { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ContactName { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ContactPhone { get; set; }

        public string ContactWeb { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Content { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Country { get; set; }

        public DateTime? EventEnd { get; set; }

        public DateTime EventStart { get; set; }

        public bool IsRecurrent { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string ItemDefaultUrl { get; set; }

        public string Location { get; set; }

        public Guid ParentId { get; set; }

        public string RecurrenceExpression { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string State { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Street { get; set; }

        [JsonConverter(typeof(LstringConverter))]
        public string Summary { get; set; }

        public Event()
        {
            this.ParentId = Guid.Empty;
        }
    }
}