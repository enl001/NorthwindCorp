using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthwindCorp.Core.Repository.Models;

namespace NorthwindCorp.Core.Repository.Services.Interfaces
{
  public interface ICategoryService
  {
    IQueryable<Category> GetCategories();


    Category GetCategoryById(int id);

    bool UpdateCategory(Category category);

    //api
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task UpdateCategoryAsync(Category category);
    Task<bool> CategoryIsExistsAsync(int id);
  }
}
