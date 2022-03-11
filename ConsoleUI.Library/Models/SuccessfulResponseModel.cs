using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsoleUI.Library.Models
{
    public class SuccessfulResponseModel
    {
        [JsonPropertyName("correlationId")]
        public string CorrelationId { get; set; }
    }
}
