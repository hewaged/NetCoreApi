using Exchange.Rates.Aud.OpenApi.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Exchange.Rates.Aud.OpenApi.Installers;

internal class RegisterSwagger : IServiceRegistration
{
    public void Register(IServiceCollection services, IConfiguration config)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Exchange.Rates.Aud.OpenApi - Daily Exchange Rates by RBA",
                Description = "Get daily exchange rates from AUD to all other currencis supplied by the Reserve Bank of Australia (RBA). For list of foreign currencies supported, please visit https://www.rba.gov.au/statistics/frequency/exchange-rates.html"
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlDocFile = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (!File.Exists(xmlDocFile))
            {
                return;
            }
            options.IncludeXmlComments(xmlDocFile);
            options.DescribeAllParametersInCamelCase();
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }
}