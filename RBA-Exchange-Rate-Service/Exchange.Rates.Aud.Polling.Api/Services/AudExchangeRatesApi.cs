using Exchange.Rates.Aud.Polling.Api.Options;
using Exchange.Rates.RBA.Contracts.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exchange.Rates.Aud.Polling.Api.Services;

public class AudExchangeRatesApi : IAudExchangeRatesApi
{
    private readonly ILogger<AudExchangeRatesApi> _logger;
    private readonly ExchangeratesApiOptions _options;
    private readonly HttpClient _httpClient;    

    public AudExchangeRatesApi(ILogger<AudExchangeRatesApi> logger, IOptions<ExchangeratesApiOptions> options, 
        HttpClient httpClient)
    {
        _logger = logger;
        _options = options.Value;
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(15);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));        
    }

    /// <summary>
    /// Get latest exchanges published daily by the RBA for units of foreign currencies per Australian dollar
    /// </summary>
    /// <param name="symbols"></param>
    /// <returns></returns>
    public async Task<AudCurrencyExchange> GetLatestRates(string symbols)
    {
        var result = new AudCurrencyExchange();
        try
        {
            var uri = new Uri(_options.Url).AbsoluteUri;
            var isValid = Uri.IsWellFormedUriString(uri, UriKind.Absolute);

            if (!isValid)
            {
                _logger.LogCritical($"RBA RSS feed Uri {uri} is invalid!");
                return result;
            }
                        
            var content = await _httpClient.GetStreamAsync(uri);
            if (content == null)
            {
                _logger.LogCritical($"Error getting exchange rates from RBA RSS feed at: {DateTimeOffset.Now}");
                return result;
            }

            var reader = new StreamReader(content);
            var text = reader?.ReadToEnd();
            if (reader == null || string.IsNullOrWhiteSpace(text))
            {
                _logger.LogCritical($"Invalid response received from from RBA RSS feed at: {DateTimeOffset.Now}");
                return result;
            }

            List<ExchangeRate> exchangeRates = ExtractExchangeRates(text);
            if (exchangeRates.Count > 0)
            {
                result = MapExchangeRateModelToAudCurrencyExchangeModel(exchangeRates, symbols);
            }

            if (!result.ExchangeRates.Any())
            {
                _logger.LogCritical($"Exchange rates not returned from RBA RSS feed for requested currencies {symbols}");
                return result;
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
        }

        return result;
    }

    /// <summary>
    /// Maps ExchangeRate model to AudCurrencyExchange model
    /// </summary>
    /// <param name="exchangeRates"></param>
    /// <param name="symbols"></param>
    /// <returns></returns>
    private AudCurrencyExchange MapExchangeRateModelToAudCurrencyExchangeModel(List<ExchangeRate> exchangeRates, 
        string symbols)
    {
        var result = new AudCurrencyExchange();
        var currencyList = symbols.Split(',').ToList();
        result.BaseCurrencyCode = exchangeRates.First().BaseCurrency;
        result.Date = exchangeRates.First().Date;
        var rates = new Dictionary<string, decimal>();
        var ratesValues = exchangeRates.Where(x => currencyList.Contains(x.TargetCurrency));
        foreach (var exchangeRate in ratesValues)
        {
            rates.Add(exchangeRate.TargetCurrency, decimal.Parse(exchangeRate.Value));
        }
        result.ExchangeRates = rates;
        return result;
    }

    /// <summary>
    /// Routine to ExtractExchangeRates from RBA exchange rate RSS feed
    /// </summary>
    /// <param name="xmlContent"></param>
    /// <returns></returns>
    private List<ExchangeRate> ExtractExchangeRates(string xmlContent)
    {
        XDocument xDocument = XDocument.Parse(xmlContent);
        XNamespace cb = "http://www.cbwiki.net/wiki/index.php/Specification_1.2/";
        XNamespace dc = "http://purl.org/dc/elements/1.1/";

        var rates = from item in xDocument.Descendants(XName.Get("item", "http://purl.org/rss/1.0/"))
                    let observation = item.Descendants(cb + "observation").FirstOrDefault()
                    let exchangeRate = item.Descendants(cb + "exchangeRate").FirstOrDefault()
                    select new ExchangeRate
                    {
                        Value = observation?.Element(cb + "value")?.Value,
                        Unit = observation?.Element(cb + "unit")?.Value,
                        Decimals = int.Parse(observation?.Element(cb + "decimals")?.Value ?? "0"),
                        BaseCurrency = exchangeRate?.Element(cb + "baseCurrency")?.Value,
                        TargetCurrency = exchangeRate?.Element(cb + "targetCurrency")?.Value,
                        Date = DateTime.Parse(item.Descendants(dc + "date").FirstOrDefault()?.Value ?? DateTime.MinValue.ToString())
                    };

        return rates.ToList();
    }
}