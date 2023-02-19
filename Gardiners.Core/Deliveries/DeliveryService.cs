using Gardiners.Core.Models;

namespace Gardiners.Core.Deliveries;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryModeDataStore _deliveryModeDataStore;
    private readonly IDeliveryPartnerDataStore _deliveryPartnerDataStore;

    public DeliveryService(IDeliveryModeDataStore deliveryModeDataStore, IDeliveryPartnerDataStore deliveryPartnerDataStore)
    {
        _deliveryModeDataStore = deliveryModeDataStore;
        _deliveryPartnerDataStore = deliveryPartnerDataStore;
    }

    public DeliverOrderResponse DeliverOrder(DeliverOrderRequest request)
    {
        var deliveryMode = _deliveryModeDataStore.GetDeliveryMode(request.DeliveryModeId);

        if (deliveryMode == null)
        {
            return new DeliverOrderResponse() { Success = false };
        }

        var deliveryPartner = _deliveryPartnerDataStore.GetDeliveryPartner(request.PartnerId);

        if (deliveryPartner == null)
        {
            return new DeliverOrderResponse() { Success = false };
        }

        var deliveryPartnerCanDeliver = deliveryPartner.AllowedDeliveryModes.Contains(deliveryMode) &&
            (!deliveryMode.ApplyOrderLimit || request.OrderTotal < deliveryPartner.OrderValueLimit) &&
            (!deliveryMode.RequiresLive || deliveryPartner.Status == Status.Live);

        if (deliveryPartnerCanDeliver)
        {
            deliveryPartner.UpdateTotalOrderValueProcessedTillDate(request.OrderTotal);
            _deliveryPartnerDataStore.UpdateDeliveryPartner(deliveryPartner);
        }

        return new DeliverOrderResponse() { Success = deliveryPartnerCanDeliver };
    }
}