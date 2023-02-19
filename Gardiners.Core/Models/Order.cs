using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardiners.Core.Models;

[Table("Orders")]
public class Order
{
    public Order(string customerID, int employeeID, string shipName, string shipAddress, string shipCity, string shipCountry)
    {
        CustomerID = customerID;
        EmployeeID = employeeID;
        ShipName = shipName;
        ShipAddress = shipAddress;
        ShipCity= shipCity;
        ShipCountry = shipCountry;
    }

    [Key]
    public int OrderID { get; set; }

    public string CustomerID { get; set; }

    public int EmployeeID { get; set; }

    public string ShipName { get; set; }

    public string ShipAddress { get; set; }

    public string ShipCity { get; set; }

    public string ShipCountry { get; set; }
}
