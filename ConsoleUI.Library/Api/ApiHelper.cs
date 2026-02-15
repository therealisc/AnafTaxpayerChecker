using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Configuration;
using System.Net.Http.Headers;

namespace ConsoleUI.Library.Api
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
    	//public string WebUrl => "AsynchWebService/api/v8/ws/tva";
    	public string WebUrl => "/api/PlatitorTvaRest/v9/tva";

        public ApiHelper()
        {
            InitializeClient();
        }

        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }

        private void InitializeClient()
        {
            //string api = ConfigurationManager.AppSettings["ApiAnafTva"];
            string api = "https://webservicesp.anaf.ro/";

            _apiClient = new HttpClient
            {
                BaseAddress = new Uri(api)
            };

            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
