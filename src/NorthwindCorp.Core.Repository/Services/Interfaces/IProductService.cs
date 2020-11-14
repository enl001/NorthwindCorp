using System.Linq;
using NorthwindCorp.Core.Repository.Models;

namespace NorthwindCorp.Core.Repository.Services.Interfaces
{
  public interface IProductService
  {
    public IQueryable<Product> GetAllProducts();

    public Product GetProductById(int id);

    public bool CreateProduct(Product product);

    public bool UpdateProduct(Product product);

  }
}
