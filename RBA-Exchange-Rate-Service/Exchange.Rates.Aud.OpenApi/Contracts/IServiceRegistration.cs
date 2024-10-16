using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exchange.Rates.Aud.OpenApi.Contracts;

public interface IServiceRegistration
{
    void Register(IServiceCollection services, IConfiguration configuration);
}