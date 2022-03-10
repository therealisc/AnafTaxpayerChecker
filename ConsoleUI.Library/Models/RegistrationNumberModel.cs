using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsoleUI.Library.Models
{
    public class RegistrationNumberModel
    {
        [JsonPropertyName("cui")]
        public int RegistrationNumber { get; set; }

        [JsonPropertyName("data")]
        public string Date { get; set; }

    }
}
