using System;
using System.Threading;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaxpayerChecker.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        string token = Environment.GetEnvironmentVariable("INFLUXDB_TOKEN");

        using var client = new InfluxDBClient("https://us-east-1-1.aws.cloud2.influxdata.com", token);
        using (var writeApi = client.GetWriteApi())
        {
            writeApi.WriteRecord("temperature,location=north value=33.0", WritePrecision.Ns, "intercom_data", "06c2e6f9e7b1be47");
        }
        
        Console.WriteLine("Complete. Return to the InfluxDB UI.");
    }
}
