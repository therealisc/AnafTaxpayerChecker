using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsoleUI.Library.Models
{
    public class TaxpayerModel
    {
        [JsonPropertyName("date_generale")]
        public CommonAttribute Attribute { get; set; }
    }

    public class CommonAttribute
    {
        [JsonPropertyName("cui")]
        public int RegistrationNumber { get; set; }

        [JsonPropertyName("data")]
        public DateTime Date { get; set; }

        [JsonPropertyName("denumire")]
        public string Name { get; set; }

        [JsonPropertyName("adresa")]
        public string Address { get; set; }

        [JsonPropertyName("telefon")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("codPostal")]
        public string PostalCode { get; set; }
    }
}
