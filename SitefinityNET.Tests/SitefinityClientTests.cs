using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SitefinityNET.Sitefinity.ServiceWrappers.ContentServices;
using System.Collections.Generic;
using SitefinityNET.Sitefinity.Models.GenericContent;
using System.Text.RegularExpressions;
using System.Configuration;

namespace SitefinityNET.Tests
{
    [TestClass]
    public class SitefinityClientTests
    {
        private static SitefinityClient sf;
        private static string username;
        private static string password;
        private static string url;

        [ClassInitialize()]
        public static void Initialize(TestContext testContext)
        {
            username = ConfigurationManager.AppSettings["Username"];
            password = ConfigurationManager.AppSettings["Password"];
            url = ConfigurationManager.AppSettings["SiteUrl"];
        }

        [TestCleanup()]
        public void Cleanup()
        {
            if (sf.IsAuthenticated)
            {
                sf.SignOut();
            }
        }

        [TestMethod]
        public void SignIn_WithValidCredentials_ReturnsToken()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string url = ConfigurationManager.AppSettings["SiteUrl"];

            sf = new SitefinityClient(username, password, url);

            sf.SignIn();

            Assert.IsTrue(sf.IsAuthenticated, "Client was not authenticated");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialsException), "Invalid username or password suceeded")]
        public void SignIn_WithInvalidCredentials_ThrowsException()
        {
            string url = ConfigurationManager.AppSettings["SiteUrl"];

            sf = new SitefinityClient("hopefullydoesnotexist", "hopefullyisnotthepassword", url);

            sf.SignIn();
        }
    }
}