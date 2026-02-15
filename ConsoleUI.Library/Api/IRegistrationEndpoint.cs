using ConsoleUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Library.Api
{
    public interface IRegistrationEndpoint
    {
        Task<string> PostRegistrationNumber(List<RegistrationNumberModel> registrationNumbers);
    }
}
