using Newtonsoft.Json;
using RestSharp;
using RestSharp.Contrib;
using RestSharp.Deserializers;
using System;
using System.Net;

namespace SitefinityNET
{
    /// <summary>
    /// Client to communicate with the native Sitefinity web services
    /// </summary>
    public class SitefinityClient
    {
        private RestClient _restClient;
        private string _baseUrl;
        private string _username;
        private string _password;
        private bool _isAuthenticated;

        public bool IsAuthenticated
        {
            get
            {
                return this._isAuthenticated;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="username">Username of the account to use</param>
        /// <param name="password">Password of the account to use</param>
        /// <param name="baseUrl">Base URL of the Sitefinity instance</param>
        public SitefinityClient(string username, string password, string baseUrl)
        {
            this._username = username;
            this._password = password;
            this._baseUrl = baseUrl;

            this._restClient = new RestClient(baseUrl);
            this._restClient.CookieContainer = new CookieContainer();
            this._restClient.ClearHandlers();
            this._restClient.AddHandler("application/json", new JsonDeserializer());
        }

        protected internal IRestResponse ExecuteRequest(IRestRequest request, bool isRetry = false)
        {
            IRestResponse response = _restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (isRetry)
                {
                    throw new UserAlreadyLoggedInException("User already logged in");
                }
                else
                {
                    SelfLogout();
                    ExecuteRequest(request, true);
                }
            }

            return response;
        }

        public string GetSitefinityVersion()
        {
            var request = new RestRequest("Sitefinity/services/SitefinityProject.svc/Version", Method.GET);

            IRestResponse response = ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<string>(response.Content);
            }

            return null;
        }

        /// <summary>
        /// Sign in to Sitefinity
        /// </summary>
        public void SignIn()
        {
            RestRequest request = new RestRequest("Sitefinity/Authenticate", Method.GET);

            IRestResponse response = _restClient.Execute(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    request = new RestRequest("Sitefinity/Authenticate/SWT?realm={realm}&redirect_uri={redirectUri}&deflate=true", Method.POST);

                    request.AddUrlSegment("realm", _baseUrl);
                    request.AddUrlSegment("redirectUri", "/Sitefinity");

                    request.AddParameter("wrap_name", _username, ParameterType.GetOrPost);
                    request.AddParameter("wrap_password", _password, ParameterType.GetOrPost);
                    request.AddParameter("sf_persistent", "true", ParameterType.GetOrPost);

                    response = _restClient.Execute(request);

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            if (response.ResponseUri.AbsolutePath == "/Sitefinity/SignOut/selflogout")
                            {
                                SelfLogout();
                            }
                            this._isAuthenticated = true;
                            break;
                        case HttpStatusCode.Unauthorized:
                            throw new InvalidCredentialsException("Invalid username or password");
                        default:
                            break;
                    }
                    break;
                case HttpStatusCode.Redirect:
                    throw new NotImplementedException("External STS not supported");
                default:
                    break;
            }
        }

        public void SelfLogout()
        {
            RestRequest request = new RestRequest("Sitefinity/SignOut/selflogout?ReturnUrl=%2fSitefinity%2fdashboard", Method.POST);

            request.AddParameter("__EVENTTARGET", "ctl04$ctl00$ctl00$ctl00$ctl00$ctl00$selfLogoutButton");
            request.AddParameter("__EVENTARGUMENT", "");

            IRestResponse response = _restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                this._isAuthenticated = false;
            }
        }

        public void SignOut()
        {
            RestRequest request = new RestRequest("Sitefinity/SignOut?sts_signout=true", Method.GET);

            IRestResponse response = ExecuteRequest(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                this._isAuthenticated = false;
            }
        }
    }
}