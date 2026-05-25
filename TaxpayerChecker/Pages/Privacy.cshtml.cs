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
            
        using var client = new InfluxDBClient(hostUrl, token: authToken);
    }
}
