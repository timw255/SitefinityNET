using RestSharp;
using RestSharp.Contrib;
using RestSharp.Deserializers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        protected internal IRestResponse ExecuteRequest(IRestRequest request)
        {
            if (!IsAuthenticated)
                throw new NotLoggedInException("Client not authenticated");

            IRestResponse response = _restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UserAlreadyLoggedInException("User already logged in");

            return response;
        }

        public string GetSitefinityVersion()
        {
            var request = new RestRequest("Sitefinity/services/SitefinityProject.svc/Version", Method.GET);
            IRestResponse response = ExecuteRequest(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            return null;
        }

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
                            this._isAuthenticated = true;
                            break;
                        case HttpStatusCode.Found:
                            //this._isAuthenticated = true;
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

        public void SignOut()
        {
            RestRequest request = new RestRequest("Sitefinity/SignOut?sts_signout=true&redirect_uri=", Method.GET);
            IRestResponse response = ExecuteRequest(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                this._isAuthenticated = false;
            }
        }
    }
}