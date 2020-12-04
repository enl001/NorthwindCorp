using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NorthwindCorp.Core.Repository.Data;
using NorthwindCorp.Core.Repository.Models;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;


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

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
      var result = await _northwindContext.Products
        .Include(product => product.Supplier)
        .Include(product => product.Category).ToListAsync();
      return result;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
      var result = await _northwindContext.Products
        .Include(product => product.Supplier)
        .Include(product => product.Category)
        .Where(product => product.ProductId == id).ToListAsync();
      return result.FirstOrDefault();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
      await _northwindContext.Products.AddAsync(product);
      var result = await _northwindContext.SaveChangesAsync();
      return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
      _northwindContext.Entry(product).State = EntityState.Modified;
      await _northwindContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Product product)
    {
      _northwindContext.Products.Remove(product);
      await _northwindContext.SaveChangesAsync();
    }

    public async Task<bool> ProductIsExists(int id)
    {
      return await _northwindContext.Products.AnyAsync(p => p.ProductId == id);
    }
  }
}
