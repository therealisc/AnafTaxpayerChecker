using System.Net.Http;

namespace ConsoleUI.Library.Api
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
		string WebUrl { get; }
    }
}
