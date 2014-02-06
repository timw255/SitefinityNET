using SitefinityNET.Sitefinity.HandlerWrappers;
using SitefinityNET.Sitefinity.Models.GenericContent;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Managers.GenericContent
{
    public class LibrariesManager
    {
        private static string _albumServiceUrl = "Sitefinity/Services/Content/AlbumService.svc";
        private static string _albumItemType = "Telerik.Sitefinity.Libraries.Model.Album";
        private static string _imageServiceUrl = "Sitefinity/Services/Content/ImageService.svc";
        private static string _imageItemType = "Telerik.Sitefinity.Libraries.Model.Image";

        private ContentServiceWrapper _service;
        private Html5UploadHandlerWrapper _uploader;
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

        public LibrariesManager(SitefinityClient sf)
        {
            this._service = new ContentServiceWrapper(sf);
            this._uploader = new Html5UploadHandlerWrapper(sf);
        }

        public Album GetAlbum(Guid albumId)
        {
            _service.ServiceUrl = _albumServiceUrl;
            Album item = _service.GetContent<Album>(albumId, _albumItemType, "", "", "", true);
            return item;
        }

        public List<dynamic> ListAlbums(string filter, int skip, int take)
        {
            _service.ServiceUrl = _albumServiceUrl;
            List<dynamic> items = _service.GetContentItems(_albumItemType, skip, take, filter, _providerName, "");
            return items;
        }

        public dynamic CreateAlbum(Album newAlbum)
        {
            _service.ServiceUrl = _albumServiceUrl;
            dynamic item = _service.SaveContent(newAlbum, Guid.Empty, _albumItemType, _providerName, "", "", true);
            return item;
        }

        public bool DeleteAlbum(Guid albumId)
        {
            _service.ServiceUrl = _albumServiceUrl;
            bool result = _service.DeleteContent(albumId, _albumItemType, _providerName, "", "", true);
            return result;
        }

        public bool DeleteAlbums(Guid[] albumIds)
        {
            _service.ServiceUrl = _albumServiceUrl;
            bool result = _service.BatchDeleteContent(albumIds, _albumItemType, _providerName, "");
            return result;
        }

        public Image GetImage(Guid ImageId)
        {
            _service.ServiceUrl = _imageServiceUrl;
            Image item = _service.GetContent<Image>(ImageId, _imageItemType, "", "", "", true);
            return item;
        }

        public List<dynamic> ListImages(string filter, int skip, int take)
        {
            _service.ServiceUrl = _imageServiceUrl;
            List<dynamic> items = _service.GetContentItems(_imageItemType, skip, take, filter, _providerName, "");
            return items;
        }

        public List<dynamic> ListImages(Guid parentId, string filter, int skip, int take)
        {
            _service.ServiceUrl = _imageServiceUrl;
            List<dynamic> items = _service.GetChildrenContentItems(parentId, _providerName, _imageItemType, _albumItemType, filter, skip, take);
            return items;
        }

        public Image CreateImage(Image newImage, Guid parentId, bool published)
        {
            _service.ServiceUrl = _imageServiceUrl;
            Image item = _service.SaveChildContent(newImage, Guid.Empty, parentId, _imageItemType, _albumItemType, _providerName, published);
            return item;
        }

        public bool DeleteImage(Guid imageId, Guid parentId)
        {
            _service.ServiceUrl = _imageServiceUrl;
            bool result = _service.DeleteChildContent(imageId, parentId, _imageItemType, _albumItemType, _providerName);
            return result;
        }

        public bool DeleteImages(Guid[] imageIds)
        {
            _service.ServiceUrl = _imageServiceUrl;
            bool result = _service.BatchDeleteContent(imageIds, _imageItemType, _providerName, "");
            return result;
        }

        public bool UploadContent(Guid parentId, Guid imageId, byte[] imageData, string fileName, string imageContentType)
        {
            bool result = _uploader.UploadContent(parentId, imageId, imageData, fileName, imageContentType);
            return result;
        }
    }
}