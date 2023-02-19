namespace Gardiners.Core.Deliveries;

public class DeliverOrderRequest
{
    public int PartnerId { get; set; }

    public decimal OrderTotal { get; set; }

    public int DeliveryModeId { get; set; }
}
