using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUI.Library.Api;
using ConsoleUI.Library.Models;

namespace ConsoleUI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IApiHelper apiHelper = new ApiHelper();
            IRegistrationEndpoint registrationEndpoint = new RegistrationEndpoint(apiHelper);

            List<RegistrationNumberModel> registrationNumbers = new()
            {
                new RegistrationNumberModel() { RegistrationNumber = 3273781, Date = "2018-01-01" }
            };

            await registrationEndpoint.PostRegistrationNumber(registrationNumbers);
        }
    }
}
