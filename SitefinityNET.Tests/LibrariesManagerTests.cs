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
        public void GetAlbums_ReturnsItems()
        {
            var items = manager.ListAlbums("", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void GetAlbums_WithFilter_ReturnsItems()
        {
            Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
            filters.Add(Utility.ContentFilter.TitleLike, "Default");

            string filterString = Utility.BuildFilterString(filters);

            var items = manager.ListAlbums(filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void GetAlbum_ReturnsCorrectItem()
        {
            Guid itemId = Guid.Parse("4ba7ad46-f29b-4e65-be17-9bf7ce5ba1fb");

            var item = manager.GetAlbum(itemId);

            Assert.AreEqual(itemId, item.Id);
        }

        [TestMethod]
        public void CreateAlbum_ReturnsNewItem()
        {
            var newSingleItem = new Album();

            string newSingleItemTitle = "New Album " + DateTime.Now.ToString();

            newSingleItem.Title = newSingleItemTitle;
            newSingleItem.UrlName = Regex.Replace(newSingleItemTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            var returnedItem = manager.CreateAlbum(newSingleItem);

            Assert.IsInstanceOfType(returnedItem.Id, typeof(Guid), "Did not return a valid Guid ID");
        }

        [TestMethod]
        public void GetImages_ReturnsItems()
        {
            var items = manager.ListImages("", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void GetImages_FromParent_ReturnsItems()
        {
            Guid parentId = Guid.Parse("4ba7ad46-f29b-4e65-be17-9bf7ce5ba1fb");

            var items = manager.ListImages(parentId, "", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void GetImages_WithFilter_ReturnsItems()
        {
            Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
            filters.Add(Utility.ContentFilter.TitleLike, "circles2");

            string filterString = Utility.BuildFilterString(filters);

            var items = manager.ListImages(filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void GetImages_FromParent_WithFilter_ReturnsItems()
        {
            Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
            filters.Add(Utility.ContentFilter.TitleLike, "circles2");

            string filterString = SitefinityNET.Utility.BuildFilterString(filters);

            Guid parentId = Guid.Parse("4ba7ad46-f29b-4e65-be17-9bf7ce5ba1fb");

            var items = manager.ListImages(parentId, filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void GetImage_ReturnsCorrectItem()
        {
            Guid itemId = Guid.Parse("639e0bfd-13ef-6fb1-b1e0-ff0000cacdaa");

            var item = manager.GetImage(itemId);

            Assert.AreEqual(itemId, item.Id);
        }

        [TestMethod]
        public void CreateImage_ReturnsNewItem()
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
                string imageUrl = "http://www.codemoar.com/images/default-source/demo/sitefinity-logo.png";

                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(imageUrl);

                    // Upload the file and tell Sitefinity which item it belongs to (via item.Id)
                    uploaded = manager.UploadContent(parentId, returnedItem.Id, imageBytes, "sitefinity-logo.png", "image/png");
                }
            }

            Assert.IsTrue(uploaded, "Did not upload sucessfully");
        }
    }
}