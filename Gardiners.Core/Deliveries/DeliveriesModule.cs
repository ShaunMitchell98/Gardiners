using Autofac;
using Gardiners.Core.Options;
using Microsoft.Extensions.Options;

namespace Gardiners.Core.Deliveries;

public class DeliveriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DeliveryService>()
            .AsImplementedInterfaces();

        builder.RegisterType<DeliveryModeDataStore>()
            .AsImplementedInterfaces();

        builder.RegisterType<StandardDeliveryPartnerDataStore>()
            .Keyed<IDeliveryPartnerDataStore>(DeliveryPartnerType.Standard);

        builder.RegisterType<PremiumDeliveryPartnerDataStore>()
            .Keyed<IDeliveryPartnerDataStore>(DeliveryPartnerType.Premium);

        builder.Register(c =>
        {
            var options = c.Resolve<IOptionsMonitor<DeliveryOptions>>();
            return c.ResolveKeyed<IDeliveryPartnerDataStore>(options.CurrentValue.DeliveryPartnerType);
        })
        .AsSelf();
    }
}
