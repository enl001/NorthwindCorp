using System.ComponentModel.DataAnnotations;

namespace NorthwindCorp.DTO
{
  public class ProductDto
  {
    [Key]
    public virtual int ProductID { get; set; }
    public virtual string ProductName { get; set; }
    public virtual int? SupplierID { get; set; }
    public virtual int? CategoryID { get; set; }
    public virtual string QuantityPerUnit { get; set; }
    public virtual decimal? UnitPrice { get; set; }
    public virtual short? UnitsInStock { get; set; }
    public virtual short? UnitsOnOrder { get; set; }
    public virtual short? ReorderLevel { get; set; }
    public virtual bool Discontinued { get; set; }
  }
}
