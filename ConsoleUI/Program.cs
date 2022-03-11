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
                .AddTransient<IRegistrationNumberModel, RegistrationNumberModel>()
                .AddTransient<ITaxpayersEndpoint, TaxpayersEndpoint>();

            _serviceProvider = services.BuildServiceProvider();


            var taxpayers = await CallApi(_serviceProvider);

            DisplayTaxpayers(taxpayers);

            Console.ReadLine();
        }

        static async Task<TaxpayersModel> CallApi(IServiceProvider services)
        {
            IRegistrationEndpoint registrationEndpoint = services.GetRequiredService<IRegistrationEndpoint>();

            List<RegistrationNumberModel> registrationNumbers = new List<RegistrationNumberModel>()
            {
                new RegistrationNumberModel() { RegistrationNumber = 3273781, Date = DateTime.Now.ToString("yyyy-mm-dd") },
                new RegistrationNumberModel() { RegistrationNumber = 3273781, Date = DateTime.Now.ToString("yyyy-mm-dd") }
            };

            var response = await registrationEndpoint.PostRegistrationNumber(registrationNumbers);

            ITaxpayersEndpoint taxpayerEndpoint = services.GetRequiredService<ITaxpayersEndpoint>();
            return await taxpayerEndpoint.GetTaxpayer(response.CorrelationId);
        }

        static void DisplayTaxpayers(TaxpayersModel taxpayers)
        {
            foreach (var taxpayer in taxpayers.Taxpayers)
            {
                foreach (var property in taxpayer.GetType().GetProperties())
                {
                    Console.WriteLine($"{property.Name}: {taxpayer.GetType().GetProperty(property.Name).GetValue(taxpayer, null)}");
                }

                Console.WriteLine();
            }
        }
    }
}
