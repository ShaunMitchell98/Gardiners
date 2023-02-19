using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardiners.Core.Models;

[Table("Products")]
public class Product
{
    public Product(string productName, int supplierID, int categoryID, string quantityPerUnit, decimal unitPrice, 
        short unitsInStock, short reorderLevel, short unitsOnOrder, bool discontinued)
    {
        ProductName = productName;
        SupplierID = supplierID;
        CategoryID = categoryID;
        QuantityPerUnit = quantityPerUnit;
        UnitPrice = unitPrice;
        UnitsInStock = unitsInStock;
        ReorderLevel = reorderLevel;
        UnitsOnOrder = unitsOnOrder;
        Discontinued = discontinued;
    }

    [Key]
    public int ProductID { get; set; }

    public string ProductName { get; set; }

    public int SupplierID { get; set; }

    public int CategoryID { get; set; }

    public string QuantityPerUnit { get; set; }

    public decimal UnitPrice { get; set; }

    public short UnitsInStock { get; set; }

    public short ReorderLevel { get; set; }

    public short UnitsOnOrder { get; set; }

    public bool Discontinued { get; set; }
}
