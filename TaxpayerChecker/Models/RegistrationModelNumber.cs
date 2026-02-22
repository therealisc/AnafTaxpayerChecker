using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxpayerChecker.Models;

public class RegistrationNumberModel : IRegistrationNumberModel
{
	[JsonPropertyName("cui")]
	public int RegistrationNumber { get; set; }

	[JsonPropertyName("data")]
	public string Date { get; set; }
}
