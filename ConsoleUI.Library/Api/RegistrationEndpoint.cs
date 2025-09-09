using ConsoleUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Net;

namespace ConsoleUI.Library.Api
{
    public class RegistrationEndpoint : IRegistrationEndpoint
    {
        private readonly IApiHelper _apiHelper;
        public RegistrationEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<SuccessfulResponseModel> PostRegistrationNumber(List<RegistrationNumberModel> registrationNumbers)
        {
            var postJson = await GetJson(registrationNumbers);
            var stringContent = new StringContent(postJson, Encoding.UTF8);

            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.PostAsync("AsynchWebService/api/v8/ws/tva", stringContent))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<SuccessfulResponseModel>(
                        await responseMessage.Content.ReadAsStringAsync());
	    	    
                    return response;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }

        private async Task<string> GetJson(object obj)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, obj, obj.GetType(), serializeOptions);
                stream.Position = 0;

                var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }
        }


        //byte[] byteArray = Encoding.UTF8.GetBytes(json);

        //var request = WebRequest.Create("https://webservicesp.anaf.ro/AsynchWebService/api/v6/ws/tva");
        //request.Method = "POST";

        //using (var reqStream = request.GetRequestStream())
        //{
        //    reqStream.Write(byteArray, 0, byteArray.Length);
        //}

        //using (var response = request.GetResponse())
        //{
        //    Console.WriteLine(((HttpWebResponse)response).StatusDescription);

        //    using (var respStream = response.GetResponseStream())
        //    {
        //        using (var reader = new StreamReader(respStream))
        //        {
        //            string data = reader.ReadToEnd();
        //            Console.WriteLine(data);
        //        }
        //    }
        //}
    }
}
