using Microsoft.EntityFrameworkCore;
using NorthwindCorp.Core.Repository.Data;
using NorthwindCorp.Core.Repository.Models;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using System.Linq;


namespace NorthwindCorp.Core.Repository.Services
{
  public class ProductService : IProductService
  {
    private readonly NorthwindContext _northwindContext;    

    public ProductService(NorthwindContext northwindContext)
    {     
      _northwindContext = northwindContext;
    }

    public IQueryable<Product> GetProducts()
    {
      var products = _northwindContext.Products
        .Include(product => product.Supplier)
        .Include(product => product.Category);

      return products;
    }

    public Product GetProductById(int id)
    {
      var products = this.GetProducts()
        .Where(product => product.ProductId == id).ToList();

      return (products.Any())
        ? products[0]
        : null;
    }

    public bool CreateProduct(Product product)
    {
      _northwindContext.Products.Add(product);
      var result = _northwindContext.SaveChanges();
      return result > 0;
    }

    public bool UpdateProduct(Product product)
    {  
      _northwindContext.Products.Update(product);
      var result = _northwindContext.SaveChanges();
      return result > 0;
    }    
  }
}
