using SitefinityNET.Sitefinity.Models.GenericContent;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Managers.GenericContent
{
    public class BlogsManager
    {
        private static string _blogServiceUrl = "Sitefinity/Services/Content/BlogService.svc";
        private static string _blogItemType = "Telerik.Sitefinity.Blogs.Model.Blog";
        private static string _blogPostServiceUrl = "Sitefinity/Services/Content/BlogPostService.svc";
        private static string _blogPostItemType = "Telerik.Sitefinity.Blogs.Model.BlogPost";

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

        public BlogsManager(SitefinityClient sf)
        {
            this._service = new ContentServiceWrapper(sf);
        }

        public List<dynamic> ListBlogs(string filter, int skip, int take)
        {
            _service.ServiceUrl = _blogServiceUrl;
            List<dynamic> items = _service.GetContentItems(_blogItemType, skip, take, filter, _providerName, "");
            return items;
        }

        public Blog GetBlog(Guid blogId)
        {
            _service.ServiceUrl = _blogServiceUrl;
            Blog item = _service.GetContent<Blog>(blogId, _blogItemType, "", "", "", true);
            return item;
        }

        public Blog CreateBlog(Blog newBlog)
        {
            _service.ServiceUrl = _blogServiceUrl;
            Blog item = _service.SaveContent(newBlog, Guid.Empty, _blogItemType, _providerName, "", "", true);
            return item;
        }

        public bool DeleteBlog(Guid blogId)
        {
            _service.ServiceUrl = _blogServiceUrl;
            bool result = _service.DeleteContent(blogId, _blogItemType, _providerName, "", "", false);
            return result;
        }

        public bool DeleteBlogs(Guid[] blogIds)
        {
            _service.ServiceUrl = _blogServiceUrl;
            bool result = _service.BatchDeleteContent(blogIds, _blogItemType, _providerName, "");
            return result;
        }

        public List<dynamic> ListBlogPosts(string filter, int skip, int take)
        {
            _service.ServiceUrl = _blogPostServiceUrl;
            List<dynamic> items = _service.GetContentItems(_blogItemType, skip, take, filter, _providerName, "");
            return items;
        }

        public List<dynamic> ListBlogPosts(Guid blogId, string filter, int skip, int take)
        {
            _service.ServiceUrl = _blogPostServiceUrl;
            List<dynamic> items = _service.GetChildrenContentItems(blogId, _providerName, _blogPostItemType, _blogItemType, filter, skip, take);
            return items;
        }

        public BlogPost GetBlogPost(Guid blogPostId)
        {
            _service.ServiceUrl = _blogPostServiceUrl;
            BlogPost item = _service.GetContent<BlogPost>(blogPostId, _blogPostItemType, "", "", "", true);
            return item;
        }

        public BlogPost CreateBlogPost(BlogPost newBlogPost, Guid parentId, bool published)
        {
            _service.ServiceUrl = _blogPostServiceUrl;
            BlogPost item = _service.SaveChildContent(newBlogPost, Guid.Empty, parentId, _blogPostItemType, _blogItemType, _providerName, published);
            return item;
        }

        public bool DeleteBlogPost(Guid blogPostId, Guid parentId)
        {
            _service.ServiceUrl = _blogPostServiceUrl;
            bool result = _service.DeleteChildContent(blogPostId, parentId, _blogPostItemType, _blogItemType, _providerName);
            return result;
        }

        public bool DeleteBlogPosts(Guid[] blogPostIds)
        {
            _service.ServiceUrl = _blogPostServiceUrl;
            bool result = _service.BatchDeleteContent(blogPostIds, _blogPostItemType, _providerName, "");
            return result;
        }
    }
}