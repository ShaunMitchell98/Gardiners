using Gardiners.Core.Models;
using System.Collections.Generic;

namespace Gardiners.Core.Deliveries;

public class PremiumDeliveryPartnerDataStore : IDeliveryPartnerDataStore
{
    public DeliveryPartner? GetDeliveryPartner(int partnerId)
    {
        if (partnerId < 0)
        {
            return null;
        }

        // Access premium partners data base to retrieve delivery partner by partner id, code removed for brevity 
        return new DeliveryPartner
        {
            AllowedDeliveryModes = new List<DeliveryMode>()
            {
                new DeliveryMode("Fast Track", false),
                new DeliveryMode("Express", true)
            },
            OrderValueLimit = 100,
        };
    }

    public void UpdateDeliveryPartner(DeliveryPartner deliveryPartner)
    {
        // Update partner in premium partners database, code removed for brevity
    }
}
