using Gardiners.Core.Models;
using System.Collections.Generic;

namespace Gardiners.Core.Deliveries;

public class StandardDeliveryPartnerDataStore : IDeliveryPartnerDataStore
{
    public DeliveryPartner? GetDeliveryPartner(int partnerId)
    {
        if (partnerId < 0)
        {
            return null;
        }
        // Access standard partners database to retrieve delivery partner by partner id, code removed for brevity 

        return new DeliveryPartner
        {
            AllowedDeliveryModes = new List<DeliveryMode>()
            {
                new DeliveryMode("Standard", false)
            },
            OrderValueLimit = 50,
        };
    }

    public void UpdateDeliveryPartner(DeliveryPartner deliveryPartner)
    {
        // Update partner in standard partners database, code removed for brevity
    }
}
