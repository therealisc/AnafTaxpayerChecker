using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json; 
using ConsoleUI.Library.Api;
using ConsoleUI.Library.Models;

namespace TaxpayerChecker.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
	private readonly IRegistrationEndpoint _registrationEndpoint;

    [BindProperty]
    public int NumericValue { get; set; }

    public List<JsonElement> FoundData { get; set; }
    public string? ErrorMessage { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IRegistrationEndpoint registrationEndpoint)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
		_registrationEndpoint = registrationEndpoint;
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

            var response = await _registrationEndpoint.PostRegistrationNumber(registrationNumbers);
			//Console.WriteLine(response);

			using var doc = JsonDocument.Parse(response);

			// Extract only the "found" property
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

            //var client = _httpClientFactory.CreateClient();
            //var response = await client.GetAsync($"https://your-api.com/endpoint/{NumericValue}");
            //if (response.IsSuccessStatusCode)
            //{
            //    var jsonString = await response.Content.ReadAsStringAsync();
            //    
            //    // Deserialize JSON to your model
            //    Result = JsonSerializer.Deserialize<ApiResultModel>(jsonString, new JsonSerializerOptions
            //    {
            //        PropertyNameCaseInsensitive = true
            //    });
            //}
            //else
            //{
            //    ErrorMessage = $"API returned error: {response.StatusCode}";
            //}
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }

        return Page();
    }
}


public class ApiResultModel
{
    public string denumire { get; set; }
    public string adresa { get; set; }
    public string telefon { get; set; }
}
