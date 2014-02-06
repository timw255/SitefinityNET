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
    public class BlogsManagerTests
    {
        private static SitefinityClient sf;
        private static BlogsManager manager;

        [ClassInitialize()]
        public static void Initialize(TestContext testContext)
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string url = ConfigurationManager.AppSettings["SiteUrl"];

            sf = new SitefinityClient(username, password, url);
            sf.SignIn();

            manager = new BlogsManager(sf);
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            sf.SignOut();
        }

        [TestMethod]
        public void BlogsManager_ListBlogs_ReturnsItems()
        {
            var items = manager.ListBlogs("", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void BlogsManager_ListBlogs_WithFilter_ReturnsItems()
        {
            Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
            filters.Add(Utility.ContentFilter.TitleEquals, "Test Blog");

            string filterString = Utility.BuildFilterString(filters);

            var items = manager.ListBlogs(filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void BlogsManager_GetBlog_ReturnsCorrectItem()
        {
            Guid itemId = Guid.Parse("8f990bfd-13ef-6fb1-b1e0-ff0000cacdaa");

            var item = manager.GetBlog(itemId);

            Assert.AreEqual(itemId, item.Id);
        }

        [TestMethod]
        public void BlogsManager_CreateBlog_ReturnsNewItem()
        {
            var newSingleItem = new Blog();

            string newSingleItemTitle = "New Blog " + DateTime.Now.ToString();

            newSingleItem.Title = newSingleItemTitle;
            newSingleItem.UrlName = Regex.Replace(newSingleItemTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            newSingleItem.AddProperty("VeryCustomShortTextField", "Test Value4");
            newSingleItem.AddProperty("VeryCustomNumberField", 427);
            newSingleItem.AddProperty("VeryCustomYesNoField", true);

            var returnedItem = manager.CreateBlog(newSingleItem);

            Assert.IsInstanceOfType(returnedItem.Id, typeof(Guid), "Did not return a valid Guid ID");
        }

        [TestMethod]
        public void BlogsManager_ListBlogPosts_ReturnsItems()
        {
            var items = manager.ListBlogPosts("", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void BlogsManager_ListBlogPosts_FromParentBlog_ReturnsItems()
        {
            Guid parentId = Guid.Parse("8f990bfd-13ef-6fb1-b1e0-ff0000cacdaa");

            var items = manager.ListBlogPosts("", 0, 50);

            Assert.IsTrue(items.Count() > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void BlogsManager_ListBlogPosts_WithFilter_ReturnsItems()
        {
            Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
            filters.Add(Utility.ContentFilter.TitleLike, "Post");

            string filterString = Utility.BuildFilterString(filters);

            var items = manager.ListBlogPosts(filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void BlogsManager_ListBlogPosts_FromParentBlog_WithFilter_ReturnsItems()
        {
            Dictionary<Utility.ContentFilter, string> filters = new Dictionary<Utility.ContentFilter, string>();
            filters.Add(Utility.ContentFilter.TitleLike, "Post");

            string filterString = Utility.BuildFilterString(filters);

            Guid parentId = Guid.Parse("8f990bfd-13ef-6fb1-b1e0-ff0000cacdaa");

            var items = manager.ListBlogPosts(parentId, filterString, 0, 50);

            Assert.IsTrue(items.Count > 0, "The count was not greater than zero");
        }

        [TestMethod]
        public void BlogsManager_GetBlogPost_ReturnsCorrectItem()
        {
            Guid itemId = Guid.Parse("479a0bfd-13ef-6fb1-b1e0-ff0000cacdaa");

            var item = manager.GetBlogPost(itemId);

            Assert.AreEqual(itemId, item.Id);
        }

        [TestMethod]
        public void BlogsManager_CreateBlogPost_ReturnsNewItem()
        {
            var newSingleItem = new BlogPost();

            string newSingleItemTitle = "New Blog Post - " + DateTime.Now.ToString();

            newSingleItem.Title = newSingleItemTitle;
            newSingleItem.UrlName = Regex.Replace(newSingleItemTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            newSingleItem.Id = Guid.Empty;

            Guid parentId = Guid.Parse("8f990bfd-13ef-6fb1-b1e0-ff0000cacdaa");

            var returnedItem = manager.CreateBlogPost(newSingleItem, parentId, true);

            Assert.IsInstanceOfType(returnedItem.Id, typeof(Guid), "Did not return a valid Guid ID");
        }

        [TestMethod]
        public void BlogsManager_ModifyBlogPost_UpdatesItemProperties()
        {
            Guid itemId = Guid.Parse("479a0bfd-13ef-6fb1-b1e0-ff0000cacdaa");

            var item = manager.GetBlogPost(itemId);

            Assert.AreEqual(itemId, item.Id);
        }
    }
}