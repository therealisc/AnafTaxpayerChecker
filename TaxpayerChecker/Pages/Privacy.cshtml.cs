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

    public void OnGet()
    {
        var hostUrl = "https://eu-central-1-1.aws.cloud2.influxdata.com";        
        var authToken = Environment.GetEnvironmentVariable("INFLUXDB_TOKEN");
        const string database = "<intercom_data>";
        
        using var client = new InfluxDBClient(hostUrl, token: authToken, database: database);

        const string record = "temperature,location=north value=60.0";
        await client.WriteRecordAsync(record: record);

        Console.WriteLine("Complete. Return to the InfluxDB UI.");
    }
}
