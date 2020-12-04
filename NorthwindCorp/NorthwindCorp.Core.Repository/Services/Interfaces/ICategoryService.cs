using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthwindCorp.Core.Repository.Models;

namespace NorthwindCorp.Core.Repository.Services.Interfaces
{
  public interface ICategoryService
  {
    IQueryable<Category> GetCategories();

    Task<IEnumerable<Category>> GetCategoriesAsync();

    Category GetCategoryById(int id);

    bool UpdateCategory(Category category);
  }
}
