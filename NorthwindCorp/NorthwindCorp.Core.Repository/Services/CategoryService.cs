using System.Linq;
using NorthwindCorp.Core.Repository.Data;
using NorthwindCorp.Core.Repository.Models;
using NorthwindCorp.Core.Repository.Services.Interfaces;


namespace NorthwindCorp.Core.Repository.Services
{
  public class CategoryService : ICategoryService
  {
    private readonly NorthwindContext _northwindContext;

    public CategoryService(NorthwindContext northwindDataContext)
    {
      _northwindContext = northwindDataContext;
    }

    public IQueryable<Category> GetCategories()
    {
      return _northwindContext.Categories;
    }
  }
}
