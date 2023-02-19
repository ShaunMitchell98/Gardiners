using Autofac;
using Autofac.Extensions.DependencyInjection;
using Gardiners.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gardiners.Core.Tests;

internal class GardinerTestFixture
{
    internal static IServiceScope CreateScope()
    {
        var configuration = new ConfigurationBuilder().Build();

        var services = new ServiceCollection();
        services.AddGardiner(configuration);

        var factory = new AutofacServiceProviderFactory(builder =>
        {
            builder.RegisterAssemblyModules(typeof(DeliveryOptions).Assembly);
        });

        var builder = factory.CreateBuilder(services);

        var provider = factory.CreateServiceProvider(builder);

        return provider.CreateScope();
    }
}
