using SitefinityNET.Sitefinity.Models.GenericContent;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Managers.GenericContent
{
    public class NewsManager
    {
        private static string _newsItemServiceUrl = "Sitefinity/Services/Content/NewsItemService.svc";
        private static string _newsItemType = "Telerik.Sitefinity.News.Model";

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

        public NewsManager(SitefinityClient sf)
        {
            this._service = new ContentServiceWrapper(sf);
        }

        public NewsItem GetNewsItem(Guid newsId)
        {
            _service.ServiceUrl = _newsItemServiceUrl;
            NewsItem item = _service.GetContent<NewsItem>(newsId, _newsItemType, "", "", "", true);
            return item;
        }

        public List<dynamic> ListNewsItems(string filter, int skip, int take)
        {
            _service.ServiceUrl = _newsItemServiceUrl;
            List<dynamic> items = _service.GetContentItems(_newsItemType, skip, take, filter, _providerName, "");
            return items;
        }

        public NewsItem CreateNewsItem(NewsItem newNews)
        {
            _service.ServiceUrl = _newsItemServiceUrl;
            NewsItem item = _service.SaveContent<NewsItem>(newNews, Guid.Empty, _newsItemType, _providerName, "", "", true);
            return item;
        }

        public bool DeleteNewsItem(Guid newsId)
        {
            _service.ServiceUrl = _newsItemServiceUrl;
            bool result = _service.DeleteContent(newsId, _newsItemType, _providerName, "", "", false);
            return result;
        }

        public bool DeleteNewsItems(Guid[] newsIds)
        {
            _service.ServiceUrl = _newsItemServiceUrl;
            bool result = _service.BatchDeleteContent(newsIds, _newsItemType, _providerName, "");
            return result;
        }
    }
}