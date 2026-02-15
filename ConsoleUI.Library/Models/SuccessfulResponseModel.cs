using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsoleUI.Library.Models
{
    public class SuccessfulResponseModel
    {
        [JsonPropertyName("date_generale")]
        public CommonAttribute Attribute { get; set; }
    }
}
