using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Gardiners.Core.Models;

[Table("Delivery_Partners")]
public class DeliveryPartner
{
    public DeliveryPartner()
    {
        AllowedDeliveryModes = new List<DeliveryMode>();
        Status = Status.Live;
        TotalOrderValueProcessedTillDate= 0;
    }

    [Key]
    public int DeliveryPartnerID { get; set; }

    public decimal OrderValueLimit { get; set; }

    public Status Status { get; set; }

    public ICollection<DeliveryMode> AllowedDeliveryModes { get; set; }

    public decimal TotalOrderValueProcessedTillDate { get; private set; }

    public void UpdateTotalOrderValueProcessedTillDate(decimal orderTotal)
    {
        TotalOrderValueProcessedTillDate += orderTotal;
    }
}
