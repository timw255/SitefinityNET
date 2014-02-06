using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Contrib;
using RestSharp.Deserializers;
using SitefinityNET.JSON;
using SitefinityNET.Sitefinity.Models;
using SitefinityNET.Sitefinity.Models.GenericContent;
using SitefinityNET.Sitefinity.Types;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SitefinityNET.Sitefinity.ServiceWrappers.ContentServices
{
    internal class ContentServiceWrapper
    {
        private string _serviceUrl;
        private SitefinityClient _sf;

        public string ServiceUrl
        {
            get
            {
                return this._serviceUrl;
            }
            set
            {
                this._serviceUrl = value;
            }
        }

        public ContentServiceWrapper(SitefinityClient sf)
        {
            this._sf = sf;
        }

        public bool BatchDeleteChildContent(Guid[] Ids, Guid parentId, string itemType, string providerName, string managerType)
        {
            var request = new RestRequest(_serviceUrl + "/parent/{parentId}/batch/?managerType={managerType}&providerName=&itemType={itemType}&provider={providerName}", Method.POST);

            request.AddUrlSegment("parentId", parentId.ToString());
            request.AddUrlSegment("providerName", providerName);
            request.AddUrlSegment("itemType", itemType);
            request.AddUrlSegment("managerType", "");

            string json = JsonConvert.SerializeObject(Ids);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public bool BatchDeleteContent(Guid[] Ids, string itemType, string providerName, string managerType)
        {
            var request = new RestRequest(_serviceUrl + "/batch/", Method.POST);

            string json = JsonConvert.SerializeObject(Ids);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public bool DeleteChildContent(Guid contentId, Guid parentId, string itemType, string parentItemType, string providerName)
        {
            var request = new RestRequest(_serviceUrl + "/parent/{parentId}/{id}/", Method.POST);

            request.AddUrlSegment("parentId", parentId.ToString());
            request.AddUrlSegment("Id", contentId.ToString());

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public bool DeleteContent(Guid contentId, string itemType, string providerName, string managerType, string version, bool published)
        {
            var request = new RestRequest(_serviceUrl + "/{contentId}/", Method.POST);

            request.AddUrlSegment("contentId", contentId.ToString());

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public bool DeleteTemp(Guid contentId, string itemType, string providerName, bool force, string workflowOperation)
        {
            var request = new RestRequest(_serviceUrl + "/temp/{contentId}/?provider={providerName}&force={force}&workflowOperation={workflowOperation}", Method.DELETE);

            request.AddUrlSegment("contentId", contentId.ToString());
            request.AddUrlSegment("providerName", providerName);
            request.AddUrlSegment("force", force.ToString().ToLower());
            request.AddUrlSegment("workflowOperation", workflowOperation);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public T GetChildContent<T>(Guid parentId, Guid contentId, string itemType, string parentItemType, string providerName, Guid newParentId, string version)
            where T : new()
        {
            RestRequest request = new RestRequest(_serviceUrl + "/parent/{parentId}/{contentId}/", Method.GET);

            request.AddUrlSegment("parentId", parentId.ToString());
            request.AddUrlSegment("contentId", contentId.ToString());
            request.AddParameter("provider", providerName);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                T deserializedItem = JsonConvert.DeserializeObject<ItemContext<T>>(response.Content).Item;
                return deserializedItem;
            }

            return default(T);
        }

        public List<dynamic> GetChildrenContentItems(Guid parentId, string providerName, string itemType, string parentItemType, string filter, int skip, int take)
        {
            var request = new RestRequest(_serviceUrl + "/parent/{parentId}/", Method.GET);

            request.AddUrlSegment("parentId", parentId.ToString());
            request.AddParameter("providerName", providerName);
            request.AddParameter("sortExpression", "LastModified DESC");
            request.AddParameter("filter", filter);
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                JObject jsonVal = JObject.Parse(response.Content) as JObject;
                dynamic jObject = jsonVal;
                List<dynamic> i = jObject.Items.ToObject<List<dynamic>>();
                return i;
            }

            return null;
        }

        public T GetContent<T>(Guid contentId, string itemType, string providerName, string managerType, string version, bool published)
            where T : new()
        {
            RestRequest request = new RestRequest(_serviceUrl + "/{contentId}/", Method.GET);

            request.AddUrlSegment("contentId", contentId.ToString());
            request.AddParameter("provider", providerName);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                T deserializedItem = JsonConvert.DeserializeObject<ItemContext<T>>(response.Content).Item;
                return deserializedItem;
            }

            return default(T);
        }

        public List<dynamic> GetContentItems(string itemType, int skip, int take, string filter, string providerName, string managerType)
        {
            RestRequest request = new RestRequest(_serviceUrl + "/", Method.GET);

            request.AddParameter("sortExpression", "LastModified DESC");
            request.AddParameter("skip", skip);
            request.AddParameter("take", take);
            request.AddParameter("filter", filter);
            request.AddParameter("providerName", providerName);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                JObject jsonVal = JObject.Parse(response.Content) as JObject;
                dynamic jObject = jsonVal;
                List<dynamic> i = jObject.Items.ToObject<List<dynamic>>();
                return i;
            }

            return null;
        }

        public bool BatchPublish(Guid[] contentIds, string providerName, string workflowOperation)
        {
            var request = new RestRequest(_serviceUrl + "/batch/publish/?provider={providerName}&workflowOperation={workflowOperation}", Method.PUT);

            request.AddUrlSegment("providerName", providerName);
            request.AddUrlSegment("workflowOperation", workflowOperation);

            string json = JsonConvert.SerializeObject(contentIds);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public bool BatchPublishChildItem(Guid[] contentIds, string providerName, string workflowOperation)
        {
            var request = new RestRequest(_serviceUrl + "/parent/{parentId}/batch/publish/?provider={providerName}&workflowOperation={workflowOperation}", Method.PUT);

            request.AddUrlSegment("providerName", providerName);
            request.AddUrlSegment("workflowOperation", workflowOperation);

            string json = JsonConvert.SerializeObject(contentIds);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public bool BatchUnpublish(Guid[] contentIds, string providerName, string workflowOperation)
        {
            var request = new RestRequest(_serviceUrl + "/batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}", Method.PUT);

            request.AddUrlSegment("providerName", providerName);
            request.AddUrlSegment("workflowOperation", workflowOperation);

            string json = JsonConvert.SerializeObject(contentIds);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public bool BatchUnpublishChildItem(Guid[] contentIds, string providerName, string workflowOperation)
        {
            var request = new RestRequest(_serviceUrl + "/parent/{parentId}/batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}", Method.PUT);

            request.AddUrlSegment("providerName", providerName);
            request.AddUrlSegment("workflowOperation", workflowOperation);

            string json = JsonConvert.SerializeObject(contentIds);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public bool CanRate(Guid contentId, string itemType, string providerName)
        {
            var request = new RestRequest(_serviceUrl + "/{contentId}/canRate/?provider={providerName}", Method.GET);

            request.AddUrlSegment("contentId", contentId.ToString());
            request.AddUrlSegment("providerName", providerName);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(response.Content);
            }

            return false;
        }

        public decimal GetRating(string itemType, Guid contentId, string providerName)
        {
            var request = new RestRequest(_serviceUrl + "/{contentId}/rating/", Method.GET);

            request.AddUrlSegment("contentId", contentId.ToString());

            request.AddParameter("provider", providerName);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<decimal>(response.Content);
            }

            return 0;
        }

        protected RatingResult SetRating(decimal value, Guid contentId, string providerName)
        {
            var request = new RestRequest(_serviceUrl + "/{contentId}/rating/?provider={providerName}", Method.PUT);

            request.AddUrlSegment("contentId", contentId.ToString());

            request.AddUrlSegment("providerName", providerName);

            request.RequestFormat = DataFormat.Json;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateParseHandling = DateParseHandling.DateTime
            };

            request.AddParameter("application/json", value, ParameterType.RequestBody);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<RatingResult>(response.Content);
            }

            return null;
        }

        public RatingResult ResetRating(string contentId, string providerName)
        {
            var request = new RestRequest(_serviceUrl + "/{contentId}/rating/?provider={providerName}", Method.DELETE);

            request.AddUrlSegment("contentId", contentId.ToString());

            request.AddUrlSegment("providerName", providerName);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<RatingResult>(response.Content);
            }

            return null;
        }

        public T SaveChildContent<T>(T content, Guid contentId, Guid parentId, string itemType, string parentItemType, string providerName, bool published)
            where T : IPersistableContentItem, new()
        {
            var request = new RestRequest(_serviceUrl + "/parent/{parentId}/{contentId}/?itemType={itemType}&provider={providerName}&parentItemType={parentItemType}&newParentId={newParentId}&workflowOperation={workflowOperation}", Method.PUT);
            
            request.AddUrlSegment("contentId", contentId.ToString());
            request.AddUrlSegment("parentId", parentId.ToString());
            request.AddUrlSegment("provider", providerName);
            request.AddUrlSegment("itemType", itemType);
            request.AddUrlSegment("providerName", providerName);
            request.AddUrlSegment("parentItemType", parentItemType);
            request.AddUrlSegment("newParentId", parentId.ToString());

            string workflowOperation = "";

            if (published)
                workflowOperation = "Publish";

            request.AddUrlSegment("workflowOperation", workflowOperation);

            request.RequestFormat = DataFormat.Json;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateParseHandling = DateParseHandling.DateTime
            };

            string json = JsonConvert.SerializeObject(new { Item = content }, serializerSettings);

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                T deserializedItem = JsonConvert.DeserializeObject<ItemContext<T>>(response.Content).Item;
                return deserializedItem;
            }

            return default(T);
        }

        public T SaveContent<T>(T content, Guid contentId, string itemType, string providerName, string managerType, string version, bool published)
            where T : IPersistableContentItem, new()
        {
            var request = new RestRequest(_serviceUrl + "/{contentId}/?itemType={itemType}&providerName={providerName}&managerType=&provider={providerName}&workflowOperation={workflowOperation}", Method.PUT);
            
            request.AddUrlSegment("contentId", contentId.ToString());
            request.AddUrlSegment("provider", providerName);
            request.AddUrlSegment("itemType", itemType);
            request.AddUrlSegment("providerName", providerName);

            string workflowOperation = "";

            if (published)
                workflowOperation = "Publish";

            request.AddUrlSegment("workflowOperation", workflowOperation);

            request.RequestFormat = DataFormat.Json;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateParseHandling = DateParseHandling.DateTime
            };

            string json = JsonConvert.SerializeObject(new { Item = content }, serializerSettings);

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                T deserializedItem = JsonConvert.DeserializeObject<ItemContext<T>>(response.Content).Item;
                return deserializedItem;
            }

            return default(T);
        }

        //public void ReorderContent(string contentId, string itemType, string providerName, string managerType, float oldPosition, float newPosition)
        //{

        //}

        //public void BatchReorderContent(Dictionary<string, float> contentIdnewOrdinal, string itemType, string providerName, string managerType)
        //{

        //}

        //public bool BatchChangeParent(string[] ids, string newParentId, string providerName, string itemType, string parentItemType)
        //{
        //    return true;
        //}
    }
}