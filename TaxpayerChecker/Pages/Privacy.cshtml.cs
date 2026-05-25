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
        
        //string token = Environment.GetEnvironmentVariable("INFLUXDB_TOKEN");
        string token = "_f7q25u8qRxg4O8bVGg8cZFR2uOtV07Z_0doZO82jeCQfX0o0fbRdSe8Ft7Mln2TZvcDFLlqh94vVK-3gE8Qxw==";
        string database = "<intercom_data>";

        using var client = new InfluxDBClient(host, token, database: database);

        const string record = "temperature,location=north value=60.0";
        
        await client.WriteRecordAsync(record: record);

        Console.WriteLine("Complete. Return to the InfluxDB UI.");
    }
}
