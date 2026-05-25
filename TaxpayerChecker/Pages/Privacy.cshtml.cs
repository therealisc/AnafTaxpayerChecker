using System;
using System.Threading;
using System.Threading.Tasks;
using InfluxDB3.Client;
using InfluxDB3.Client.Write;
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

    public async void OnGet()
    {
        using var client = new InfluxDBClient(new ClientConfig
        {
            Host = "https://us-east-1-1.aws.cloud2.influxdata.com",
            Token = Environment.GetEnvironmentVariable("INFLUXDB_TOKEN"),
            Database = "intercom_data",
            
            QueryOptions = new QueryOptions
            {
                DisableGrpcCompression = true
            }
        });

        const string record = "temperature,location=north value=60.0";
        
        await client.WriteRecordAsync(record: record);

        Console.WriteLine("Complete. Return to the InfluxDB UI.");
    }
}
