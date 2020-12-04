using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthwindCorp.Core.Repository.Models;

namespace NorthwindCorp.Core.Repository.Services.Interfaces
{
  public interface IProductService
  {
    public IQueryable<Product> GetProducts();

    public Product GetProductById(int id);

    public bool CreateProduct(Product product);

    public bool UpdateProduct(Product product);

    // api
    public Task<IEnumerable<Product>> GetProductsAsync();
    public Task<Product> GetProductByIdAsync(int id);
    public Task<Product> CreateProductAsync(Product product);
    public Task UpdateProductAsync(Product product);
    public Task DeleteProductAsync(Product product);
    public Task<bool> ProductIsExists(int id);

  }
}
