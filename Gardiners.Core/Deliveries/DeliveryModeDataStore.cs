using Gardiners.Core.Models;

namespace Gardiners.Core.Deliveries;

public class DeliveryModeDataStore : IDeliveryModeDataStore
{
    public DeliveryMode? GetDeliveryMode(int deliveryModeId)
    {
        if (deliveryModeId < 0)
        {
            return null;
        }

        return new DeliveryMode("Standard", false);
    }
}
