using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using System.Collections.Generic;
using SitefinityNET.Sitefinity.Models.GenericContent;
using System.Text.RegularExpressions;
using SitefinityNET.Managers.GenericContent;
using System.Configuration;

namespace SitefinityNET.Tests
{
    [TestClass]
    public class NewsManagerTests
    {
        private static SitefinityClient sf;
        private static NewsManager manager;

        [ClassInitialize()]
        public static void Initialize(TestContext testContext)
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string url = ConfigurationManager.AppSettings["SiteUrl"];

            sf = new SitefinityClient(username, password, url);

            sf.SignIn();

            manager = new NewsManager(sf);
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            sf.SignOut();
        }

        [TestMethod]
        public void NewsServiceWrapper_GetNewsItems_ReturnsItems()
        {
            var items = manager.ListNewsItems("", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void NewsServiceWrapper_GetNewsItems_WithFilter_ReturnsItems()
        {
            Dictionary<SitefinityNET.Utility.ContentFilter, string> filters = new Dictionary<SitefinityNET.Utility.ContentFilter, string>();
            filters.Add(SitefinityNET.Utility.ContentFilter.TitleLike, "Test");

            string filterString = SitefinityNET.Utility.BuildFilterString(filters);

            var items = manager.ListNewsItems(filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void NewsServiceWrapper_GetNewsItem_ReturnsCorrectItem()
        {
            Guid itemId = Guid.Parse("dfa20bfd-13ef-6fb1-b1e0-ff0000cacdaa");

            var item = manager.GetNewsItem(itemId);

            Assert.AreEqual(itemId, item.Id);
        }

        //[TestMethod]
        //public void NewsServiceWrapper_UpdateNewsItem_UpdatesItem()
        //{
        //    Guid itemId = Guid.Parse("dfa20bfd-13ef-6fb1-b1e0-ff0000cacdaa");

        //    var item = manager.GetNewsItem(itemId);

        //    string newDescription = "Updated description " + DateTime.Now.ToString();

        //    item.Description = newDescription;

        //    manager.SaveNewsItem(item);

        //    item = manager.GetNewsItem(itemId);

        //    Assert.AreEqual(newDescription, item.Description);
        //}

        [TestMethod]
        public void NewsServiceWrapper_CreateNewsItem_ReturnsNewItem()
        {
            var newSingleItem = new NewsItem();

            string newSingleItemTitle = "New News " + DateTime.Now.ToString();

            newSingleItem.Title = newSingleItemTitle;
            newSingleItem.UrlName = Regex.Replace(newSingleItemTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            var returnedItem = manager.CreateNewsItem(newSingleItem);

            Assert.IsInstanceOfType(returnedItem.Id, typeof(Guid), "Did not return a valid Guid ID");
        }
    }
}