using Exchange.Rates.RBA.Contracts.Messages;
using Exchange.Rates.Aud.OpenApi.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Exchange.Rates.Aud.OpenApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class ExchangeRatesAudController : ControllerBase
{
    private readonly ILogger<ExchangeRatesAudController> _logger;
    private readonly IRequestClient<ISubmitAudExchangeRateSymbols> _submitAudExchangeRateRequestClient;

    public ExchangeRatesAudController(ILogger<ExchangeRatesAudController> logger,
        IRequestClient<ISubmitAudExchangeRateSymbols> submitAudExchangeRateRequestClient)
    {
        _logger = logger;
        _submitAudExchangeRateRequestClient = submitAudExchangeRateRequestClient;
    }

    /// <summary>
    /// AudBaseRates endpoint
    /// </summary>
    /// <remarks>
    /// Exchange rates quoted against the AUD as comma separated list. Example Symbols list: NZD,USD,SGD
    /// </remarks>
    /// <param name="model">Currency symbols</param>
    /// <returns></returns>
    /// <response code="200">Returned if everything is ok</response>
    /// <response code="400">Returned if something went wrong</response>
    [HttpGet("audbaserates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AudBaseRates([FromQuery] SymbolsSubmissionModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model");
                return BadRequest(ModelState);
            }

            // https://masstransit-project.com/usage/requests.html#request-client
            var (accepted, rejected) = await _submitAudExchangeRateRequestClient.GetResponse<IAudExchangeRatesAccepted, IAudExchangeRatesRejected>(new
            {
                EventId = NewId.NextGuid(),
                InVar.Timestamp,
                Symbols = model.Symbols.Split(',')
            }).ConfigureAwait(false);

            if (accepted.IsCompletedSuccessfully)
            {
                var response = await accepted.ConfigureAwait(false);
                return Ok(response.Message.CurrencyExchange);
            }

            if (accepted.IsCompleted)
            {
                await accepted.ConfigureAwait(false);
                const string ERR_MESSAGE = "Symbols were not accepted. Please check the syntax!";
                _logger.LogError(ERR_MESSAGE);
                return Problem(ERR_MESSAGE);
            }
            else
            {
                var response = await rejected.ConfigureAwait(false);
                _logger.LogError(response.Message.Reason);
                return BadRequest(response.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}