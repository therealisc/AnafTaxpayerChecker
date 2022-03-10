using ConsoleUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.IO;

namespace ConsoleUI.Library.Api
{
    public class RegistrationEndpoint : IRegistrationEndpoint
    {
        private readonly IApiHelper _apiHelper;
        public RegistrationEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostRegistrationNumber(List<RegistrationNumberModel> registrationNumbers)
        {
            var jsonObject = await GetJson(registrationNumbers);
            var stringContent = new StringContent(jsonObject);



            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.PostAsync("AsynchWebService/api/v6/ws/tva", stringContent))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine(responseMessage.ReasonPhrase);
                    Console.WriteLine(await responseMessage.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }

        private static async Task<string> GetJson(object obj)
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
