using ConsoleUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleUI.Library.Api
{
    public class TaxpayersEndpoint : ITaxpayersEndpoint
    {
        private readonly IApiHelper _apiHelper;

        public TaxpayersEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<TaxpayersModel> GetTaxpayer(string correlationId)
        {
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync($"AsynchWebService/api/v6/ws/tva?id={correlationId}"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<TaxpayersModel>(
                        await responseMessage.Content.ReadAsStringAsync());

                    return response;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }
    }
}
