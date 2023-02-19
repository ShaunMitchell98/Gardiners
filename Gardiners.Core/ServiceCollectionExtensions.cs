using Gardiners.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gardiners.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGardiner(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddOptions()
            .Configure<DeliveryOptions>(configuration);
    }
}
