using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindCorp.Data;
using NorthwindCorp.Services;

namespace NorthwindCorp.Controllers
{
  [Route("categories")]
  public class CategoryController : Controller
  {
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    [HttpGet("/list")]
    [ProducesResponseType(200)]
    public IActionResult GetCategoryList()
    {
      var categoryList = _categoryService.GetAllCategories();
      return View(categoryList);
    }

    //[HttpGet("/list")]
    //[ProducesResponseType(200)]
    //public IActionResult GetCategoryList()
    //{
    //  var categoryList = _categoryService.GetAllCategories();
    //  return View(categoryList);
    //}
  }
}
