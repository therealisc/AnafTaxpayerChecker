using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUI.Library.Api;
using ConsoleUI.Library.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace ConsoleUI
{
    internal class Program
    {
        private static IServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services
                .AddSingleton<IApiHelper, ApiHelper>()
                .AddSingleton<IRegistrationEndpoint, RegistrationEndpoint>()
                .AddTransient<IRegistrationNumberModel, RegistrationNumberModel>();

            _serviceProvider = services.BuildServiceProvider();


            var taxpayers = await CallApi(_serviceProvider);
        }

        static async Task<TaxpayersModel> CallApi(IServiceProvider services)
        {
            IRegistrationEndpoint registrationEndpoint = services.GetRequiredService<IRegistrationEndpoint>();

            List<RegistrationNumberModel> registrationNumbers = new List<RegistrationNumberModel>()
            {
                new RegistrationNumberModel() { RegistrationNumber = 6719278, Date = DateTime.Now.ToString("yyyy-mm-dd") },
            };

            var response = await registrationEndpoint.PostRegistrationNumber(registrationNumbers);

			Console.WriteLine(response);

			return new TaxpayersModel();
        }
    }
}
