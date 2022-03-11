using ConsoleUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Library.Api
{
    public interface ITaxpayersEndpoint
    {
        Task<TaxpayersModel> GetTaxpayer(string correlationId);
    }
}