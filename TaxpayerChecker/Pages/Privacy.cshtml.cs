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

        const string database = "<intercom_data>";

        var points = new[]
            {
                PointData.Measurement("census")
                    .SetTag("location", "Klamath")
                    .SetField("bees", 23),
                PointData.Measurement("census")
                    .SetTag("location", "Portland")
                    .SetField("ants", 30),
                PointData.Measurement("census")
                    .SetTag("location", "Klamath")
                    .SetField("bees", 28),
                PointData.Measurement("census")
                    .SetTag("location", "Portland")
                    .SetField("ants", 32),
                PointData.Measurement("census")
                    .SetTag("location", "Klamath")
                    .SetField("bees", 29),
                PointData.Measurement("census")
                    .SetTag("location", "Portland")
                    .SetField("ants", 40)
            };

        foreach (var point in points)
        {
            await client.WritePointAsync(point: point, database: database);
    
            Thread.Sleep(1000); // separate points by 1 second
        }

            Console.WriteLine("Complete. Return to the InfluxDB UI.");
    }
}
