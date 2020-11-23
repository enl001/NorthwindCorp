using System.Linq;
using NorthwindCorp.Core.Repository.Models;

namespace NorthwindCorp.Core.Repository.Services.Interfaces
{
  public interface ICategoryService
  {
    IQueryable<Category> GetCategories();
    Category GetCategoryById(int id);

    bool UpdateCategory(Category category);
  }
}
