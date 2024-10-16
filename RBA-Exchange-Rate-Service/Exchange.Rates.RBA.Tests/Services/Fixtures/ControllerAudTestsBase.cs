using Exchange.Rates.RBA.Tests.Services.Factories;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace Exchange.Rates.RBA.Tests.Services.Fixtures;

/// <summary>
/// Base Controller tests IClassFixture
/// </summary>
public class ControllerAudTestsBase : IClassFixture<WebApiAudTestFactory>
{
    protected HttpClient Client;

    public ControllerAudTestsBase(WebApiAudTestFactory factory)
    {
        Client = factory.CreateClient();
        Client.Timeout = TimeSpan.FromSeconds(15);
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}