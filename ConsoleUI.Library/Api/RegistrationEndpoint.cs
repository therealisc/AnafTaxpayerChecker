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

        //public async Task<SuccessfulResponseModel> PostRegistrationNumber(List<RegistrationNumberModel> registrationNumbers)
        public async Task<string> PostRegistrationNumber(List<RegistrationNumberModel> registrationNumbers)
        {
            var postJson = await GetJson(registrationNumbers);
            var stringContent = new StringContent(postJson, Encoding.UTF8);

            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.PostAsync(_apiHelper.WebUrl, stringContent))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    //var response = JsonSerializer.Deserialize<SuccessfulResponseModel>(
                        //await responseMessage.Content.ReadAsStringAsync());

					var response = await responseMessage.Content.ReadAsStringAsync();
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
    }
}
