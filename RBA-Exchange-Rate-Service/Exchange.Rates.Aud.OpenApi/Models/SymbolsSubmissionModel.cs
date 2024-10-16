using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Exchange.Rates.Aud.OpenApi.Models;

public sealed class SymbolsSubmissionModel
{
    /// <summary>
    /// Currency symbols list. Ex: NZD,USD,SGD
    /// </summary>
    [Required]
    [FromQuery(Name = "Symbols")]
    public string Symbols { get; set; }
}