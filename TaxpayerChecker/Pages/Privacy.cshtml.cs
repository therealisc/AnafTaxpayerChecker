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
        string host = "https://us-east-1-1.aws.cloud2.influxdata.com";
        
        string token = Environment.GetEnvironmentVariable("INFLUXDB_TOKEN");
        
        string database = "<intercom_data>";

        
        string bucket = "intercom_data";
        string org = "xtermost";

        using var client = new InfluxDBClient(host, token, database: database);
        var writeApi = client.GetWriteApiAsync();
        
        var lineProtocol = "temperature,location=warehouse value=18.2";
        await writeApi.WriteRecordAsync(lineProtocol, WritePrecision.Ns, bucket, org);

        Console.WriteLine("Complete. Return to the InfluxDB UI.");
    }
}
