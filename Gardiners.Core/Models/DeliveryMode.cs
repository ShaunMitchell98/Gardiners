using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardiners.Core.Models;

[Table("Delivery_Modes")]
public class DeliveryMode
{
    public DeliveryMode(string name, bool requiresLive)
    {
        Name = name;
        RequiresLive = requiresLive;
        ApplyOrderLimit = false;
    }

    [Key]
    public int DeliveryModeID { get; set; }

    public string Name { get; set; }

    public bool ApplyOrderLimit { get; set; }

    public bool RequiresLive { get; set; }
}
