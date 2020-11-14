using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NorthwindCorp.Core.Repository.Services.Interfaces;

namespace NorthwindCorp.Web.Controllers
{
  [Route("[controller]")]
  public class CategoriesController : Controller
  {
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    [HttpGet()]
    [ProducesResponseType(200)]
    public IActionResult Index()
    {
      var categoryList = _categoryService.GetCategories().ToList();
      return View(categoryList);
    }
  }
}
