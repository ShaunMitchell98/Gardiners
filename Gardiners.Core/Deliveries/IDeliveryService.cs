namespace Gardiners.Core.Deliveries;

public interface IDeliveryService
{
    DeliverOrderResponse DeliverOrder(DeliverOrderRequest request);
}