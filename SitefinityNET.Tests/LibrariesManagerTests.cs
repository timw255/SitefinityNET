using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SitefinityNET.Sitefinity.Models.GenericContent;
using System.Text.RegularExpressions;
using SitefinityNET.Managers.GenericContent;
using System.IO;
using System.Net;
using System.Configuration;

namespace SitefinityNET.Tests
{
    [TestClass]
    public class LibrariesManagerTests
    {
        private static SitefinityClient sf;
        private static LibrariesManager manager;

        [ClassInitialize()]
        public static void Initialize(TestContext testContext)
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string url = ConfigurationManager.AppSettings["SiteUrl"];

            sf = new SitefinityClient(username, password, url);
            sf.SignIn();

            manager = new LibrariesManager(sf);
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            sf.SignOut();
        }

        [TestMethod]
        public void AlbumServiceWrapper_GetAlbums_ReturnsItems()
        {
            var items = manager.ListAlbums("", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void AlbumServiceWrapper_GetAlbums_WithFilter_ReturnsItems()
        {
            Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
            filters.Add(Utility.ContentFilter.TitleLike, "Default");

            string filterString = Utility.BuildFilterString(filters);

            var items = manager.ListAlbums(filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void AlbumServiceWrapper_GetAlbum_ReturnsCorrectItem()
        {
            Guid itemId = Guid.Parse("4ba7ad46-f29b-4e65-be17-9bf7ce5ba1fb");

            var item = manager.GetAlbum(itemId);

            Assert.AreEqual(itemId, (Guid)item.Id);
        }

        [TestMethod]
        public void AlbumServiceWrapper_CreateAlbum_ReturnsNewItem()
        {
            var newSingleItem = new Album();

            string newSingleItemTitle = "New Album " + DateTime.Now.ToString();

            newSingleItem.Title = newSingleItemTitle;
            newSingleItem.UrlName = Regex.Replace(newSingleItemTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            var returnedItem = manager.CreateAlbum(newSingleItem);

            Assert.IsInstanceOfType((Guid)returnedItem.Id, typeof(Guid), "Did not return a valid Guid ID");
        }

        [TestMethod]
        public void ImageServiceWrapper_GetImages_ReturnsItems()
        {
            var items = manager.ListImages("", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void ImageServiceWrapper_GetImages_FromParent_ReturnsItems()
        {
            Guid parentId = Guid.Parse("4ba7ad46-f29b-4e65-be17-9bf7ce5ba1fb");

            var items = manager.ListImages(parentId, "", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void ImageServiceWrapper_GetImages_WithFilter_ReturnsItems()
        {
            Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
            filters.Add(Utility.ContentFilter.TitleLike, "circles2");

            string filterString = Utility.BuildFilterString(filters);

            var items = manager.ListImages(filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void ImageServiceWrapper_GetImages_FromParent_WithFilter_ReturnsItems()
        {
            Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
            filters.Add(Utility.ContentFilter.TitleLike, "circles2");

            string filterString = SitefinityNET.Utility.BuildFilterString(filters);

            Guid parentId = Guid.Parse("4ba7ad46-f29b-4e65-be17-9bf7ce5ba1fb");

            var items = manager.ListImages(parentId, filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void ImageServiceWrapper_GetImage_ReturnsCorrectItem()
        {
            Guid itemId = Guid.Parse("639e0bfd-13ef-6fb1-b1e0-ff0000cacdaa");

            var item = manager.GetImage(itemId);

            Assert.AreEqual(itemId, (Guid)item.Id);
        }

        [TestMethod]
        public void ImageServiceWrapper_CreateImage_ReturnsNewItem()
        {
            var newSingleItem = new Image();

            string newSingleItemTitle = "New Image - " + DateTime.Now.ToString();

            newSingleItem.Title = newSingleItemTitle;
            newSingleItem.UrlName = Regex.Replace(newSingleItemTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            Guid parentId = Guid.Parse("4ba7ad46-f29b-4e65-be17-9bf7ce5ba1fb");

            Image returnedItem = manager.CreateImage(newSingleItem, parentId, false);

            bool uploaded = false;

            if (returnedItem.Id != Guid.Empty)
            {
                Stream imageDataStream = DownloadRemoteImageFile("http://www.codemoar.com/Sitefinity/WebsiteTemplates/CodeMoar/App_Themes/CodeMoar/images/features/circles2.png");

                byte[] imageData = Utility.ReadFully(imageDataStream);

                uploaded = manager.UploadContent(parentId, returnedItem.Id, imageData, "circles2.png", "image/png");
            }

            Assert.IsTrue(uploaded, "Did not upload sucessfully");
        }

        public static Stream DownloadRemoteImageFile(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                return null;
            }

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                // the remote file was found, download it
                Stream inputStream = response.GetResponseStream();

                return inputStream;
            }
            else
                return null;
        }
    }
}
