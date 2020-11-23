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

    public Category GetCategoryById(int id)
    {
      var categories = this.GetCategories()
        .Where(category => category.CategoryId == id).ToList();

      return (categories.Any())
        ? categories[0]
        : null;
    }

    public bool UpdateCategory(Category category)
    {
      _northwindContext.Categories.Update(category);
      var result = _northwindContext.SaveChanges();
      return result > 0;
    }

  }
}
