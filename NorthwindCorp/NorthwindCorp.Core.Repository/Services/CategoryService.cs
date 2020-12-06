using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    // api
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
      return await _northwindContext.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
      var categories = await _northwindContext.Categories
        .Where(category => category.CategoryId == id).ToListAsync();

      return categories.FirstOrDefault();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
      _northwindContext.Entry(category).State = EntityState.Modified;
      await _northwindContext.SaveChangesAsync();
    }

    public async Task<bool> CategoryIsExistsAsync(int id)
    {
      return await _northwindContext.Categories.AnyAsync(c => c.CategoryId == id);
    }
  }
}
