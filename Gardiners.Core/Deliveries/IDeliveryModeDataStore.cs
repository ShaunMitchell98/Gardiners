using Gardiners.Core.Models;

namespace Gardiners.Core.Deliveries;

public interface IDeliveryModeDataStore
{
    public DeliveryMode? GetDeliveryMode(int deliveryModeId);
}
