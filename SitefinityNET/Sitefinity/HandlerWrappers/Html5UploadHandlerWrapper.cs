using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.HandlerWrappers
{
    public class Html5UploadHandlerWrapper
    {
        private static string _html5UploadUrl = "/Telerik.Sitefinity.Html5UploadHandler.ashx";

        private SitefinityClient _sf;

        public Html5UploadHandlerWrapper(SitefinityClient sf)
        {
            this._sf = sf;
        }

        public bool UploadContent(Guid parentId, Guid contentId, byte[] contentData, string fileName, string imageContentType)
        {
            var request = new RestRequest(_html5UploadUrl, Method.POST);

            request.AddParameter("ContentType", "Telerik.Sitefinity.Libraries.Model.Image");
            request.AddParameter("LibraryId", parentId.ToString());
            request.AddParameter("ContentId", contentId.ToString());
            request.AddParameter("Workflow", "Upload");
            request.AddParameter("ProviderName", "OpenAccessDataProvider");

            request.AddFile("uploadInput", contentData, fileName, "application/octet-stream");

            IRestResponse response = _sf.ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}