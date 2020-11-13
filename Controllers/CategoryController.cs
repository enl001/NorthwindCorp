using Microsoft.AspNetCore.Mvc;
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
  }
}
