using Gardiners.Core.Models;

namespace Gardiners.Core.Deliveries;

public interface IDeliveryPartnerDataStore
{
    public DeliveryPartner? GetDeliveryPartner(int partnerId);

    public void UpdateDeliveryPartner(DeliveryPartner deliveryPartner);
}
