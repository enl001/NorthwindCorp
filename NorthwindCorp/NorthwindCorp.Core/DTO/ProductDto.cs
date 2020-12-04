namespace NorthwindCorp.Core.DTO
{
  public sealed class ProductDto
  {
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }
    public CategoryDto Category { get; set; }
    public SupplierDto Supplier { get; set; }
  }
}
