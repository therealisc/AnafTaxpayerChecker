using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json; 
using System.Text; 
using TaxpayerChecker.Pages;
using TaxpayerChecker.Models;

namespace TaxpayerChecker.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    [BindProperty]
    public int NumericValue { get; set; }

    public List<JsonElement> FoundData { get; set; }
    public string? ErrorMessage { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            List<RegistrationNumberModel> registrationNumbers = new List<RegistrationNumberModel>()
            {
                new RegistrationNumberModel() { RegistrationNumber = NumericValue, Date = DateTime.Now.ToString("yyyy-mm-dd") },
            };

            var client = _httpClientFactory.CreateClient();
    	    string webUrl = "https://webservicesp.anaf.ro/api/PlatitorTvaRest/v9/tva";

            var postJson = await GetJson(registrationNumbers);
            var stringContent = new StringContent(postJson, Encoding.UTF8);

            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            using (HttpResponseMessage responseMessage = await client.PostAsync(webUrl, stringContent))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
					var response = await responseMessage.Content.ReadAsStringAsync();
					using var doc = JsonDocument.Parse(response);

					//// Extract only the "found" property
					if (doc.RootElement.TryGetProperty("found", out JsonElement foundElement))
					{
							if (foundElement.ValueKind == JsonValueKind.Array)
							{
									FoundData = foundElement.EnumerateArray().Select(e => e.Clone()).ToList();
							}
							else
							{
									ErrorMessage = "'found' is not an array";
							}
					}
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }

        return Page();
    }

	private async Task<string> GetJson(object obj)
	{
		var serializeOptions = new JsonSerializerOptions
		{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
									 WriteIndented = true
		};

		using (var stream = new MemoryStream())
		{
			await JsonSerializer.SerializeAsync(stream, obj, obj.GetType(), serializeOptions);
			stream.Position = 0;

			var reader = new StreamReader(stream);
			return await reader.ReadToEndAsync();
		}
	}
}


public class ApiResultModel
{
    public string denumire { get; set; }
    public string adresa { get; set; }
    public string telefon { get; set; }
}
