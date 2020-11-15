using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

#nullable disable

namespace NorthwindCorp.Core.Repository.Models
{
  public partial class Product
  {
    public Product()
    {
      OrderDetails = new HashSet<OrderDetail>();
    }
    [Key]
    public int ProductId { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters long")]
    public string ProductName { get; set; }

    [Display(Name = "Supplier")]
    [Required(ErrorMessage ="Select supplier please")]
    public int? SupplierId { get; set; }

    [Display(Name = "Category")]
    [Required(ErrorMessage = "Select category please")]
    public int? CategoryId { get; set; }

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

    public virtual Category Category { get; set; }
    public virtual Supplier Supplier { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    [NotMapped]
    public virtual IEnumerable<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
    [NotMapped]
    public virtual IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
  }
}
