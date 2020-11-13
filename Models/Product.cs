using System;
using System.ComponentModel.DataAnnotations;


namespace NorthwindCorp.Models
{
  public class Product
  {
    public int Id { get; set; }

    [Display(Name = "Supplier")]
    public int? SupplierId { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters long")]
    public string Name { get; set; }

    public string SupplierName { get; set; }

    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    public string CategoryName { get; set; }

    [StringLength(100)]
    public string QuantityPerUnit { get; set; }

    [DataType(DataType.Currency)]
    [Range(0, 100000, ErrorMessage = "Provide a correct price please")]
    public decimal? UnitPrice { get; set; }
    [Range(0, 1000)]
    public short? UnitsInStock { get; set; }
    [Range(0, 1000)]
    public short? UnitsOnOrder { get; set; }
    [Range(0, 10)]
    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }
  }
}
