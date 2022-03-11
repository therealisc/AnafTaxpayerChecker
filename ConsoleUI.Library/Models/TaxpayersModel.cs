using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsoleUI.Library.Models
{
    public class TaxpayersModel
    {
        [JsonPropertyName("found")]
        public List<TaxpayerModel> Taxpayers { get; set; } = new List<TaxpayerModel>();
    }
}
