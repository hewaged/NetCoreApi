using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Rates.Aud.OpenApi.Filters;

/// <summary>
/// Corresponding to Controller's API document description information
/// </summary>
public class SwaggerDocumentFilter : IDocumentFilter
{
    private const string DOCS_URI = "https://www.rba.gov.au/statistics/frequency/exchange-rates.html";

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var tags = new List<OpenApiTag>
          {
              new()
              {
                  Name = "ExchangeRatesAud",
                  Description = "Daily Exchange Rates published by the Reserve Bank of Australia (RBA) for units of foreign currencies per Australian Dollar",
                  ExternalDocs = new OpenApiExternalDocs
                  {
                      Description = "Read more",
                      Url = new Uri(DOCS_URI)
                  }
              }
          };

        // Sort in ascending order by AssemblyName
        swaggerDoc.Tags = tags.OrderBy(x => x.Name).ToList();
    }
}