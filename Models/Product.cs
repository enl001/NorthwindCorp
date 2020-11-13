using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

    [StringLength(1000)]
    public string QuantityPerUnit { get; set; }

    [DataType(DataType.Currency, ErrorMessage = "Should be a number")]
    [Range(0, 100000, ErrorMessage = "Provide a correct price please")]
    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }
    
    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }
  }
}
