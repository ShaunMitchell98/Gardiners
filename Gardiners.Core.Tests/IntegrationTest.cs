using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Gardiners.Core.Tests;

[Collection("IntegrationTests")]
public abstract class IntegrationTest : IAsyncLifetime
{
    private IServiceScope? _scope;

    public Task InitializeAsync()
    {
        _scope = GardinerTestFixture.CreateScope();
        return Task.CompletedTask;
    }

    public TService? GetService<TService>()
    {
        return _scope!.ServiceProvider.GetService<TService>();
    }

    public Task DisposeAsync()
    {
        _scope!.Dispose();
        return Task.CompletedTask;
    }
}
