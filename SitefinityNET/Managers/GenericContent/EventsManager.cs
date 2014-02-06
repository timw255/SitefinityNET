using SitefinityNET.Sitefinity.Models.GenericContent;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Managers.GenericContent
{
    class EventsManager
    {
        private static string _eventServiceUrl = "Sitefinity/Services/Content/EventService.svc";
        private static string _eventItemType = "Telerik.Sitefinity.Events.Model";

        private ContentServiceWrapper _service;
        private string _providerName = "OpenAccessDataProvider";

        public string ProviderName
        {
            get
            {
                return this._providerName;
            }
            set
            {
                this._providerName = value;
            }
        }

        public EventsManager(SitefinityClient sf)
        {
            this._service = new ContentServiceWrapper(sf);
        }

        public Event GetEvent(Guid eventId)
        {
            _service.ServiceUrl = _eventServiceUrl;
            Event item = _service.GetContent<Event>(eventId, _eventItemType, "", "", "", true);
            return item;
        }

        public List<dynamic> ListEventItems(string filter, int skip, int take)
        {
            _service.ServiceUrl = _eventServiceUrl;
            List<dynamic> items = _service.GetContentItems(_eventItemType, skip, take, filter, _providerName, "");
            return items;
        }

        public Event CreateEvent(Event newEvent)
        {
            _service.ServiceUrl = _eventServiceUrl;
            dynamic item = _service.SaveContent<Event>(newEvent, Guid.Empty, _eventItemType, _providerName, "", "", true);
            return item;
        }

        public bool DeleteEvent(Guid eventId)
        {
            _service.ServiceUrl = _eventServiceUrl;
            bool result = _service.DeleteContent(eventId, _eventItemType, _providerName, "", "", false);
            return result;
        }

        public bool DeleteEvents(Guid[] eventIds)
        {
            _service.ServiceUrl = _eventServiceUrl;
            bool result = _service.BatchDeleteContent(eventIds, _eventItemType, _providerName, "");
            return result;
        }
    }
}
