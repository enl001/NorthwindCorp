using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindCorp.Data;
using NorthwindCorp.Models;

namespace NorthwindCorp.Services
{
  public class CategoryService
  {
    private NorthwindDataContext _northwindDataContext;

    public CategoryService(NorthwindDataContext northwindDataContext)
    {
      _northwindDataContext = northwindDataContext;
    }

    public IEnumerable<Category> GetAllCategories()
    {
      return _northwindDataContext.Categories.Select(category => new Category
      {
        Name = category.CategoryName,
        Description = category.Description,
        Picture = category.Picture
      }).ToList();
    }

    public IEnumerable<SelectListItem> GetSelectListCategories()
    {
      return _northwindDataContext.Categories.Select(category => new SelectListItem
      {
        Text = category.CategoryName,
        Value = category.CategoryId.ToString()
      }).ToList();
    }

  }
}
