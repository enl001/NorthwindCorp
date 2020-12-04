using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthwindCorp.Core.DTO;

namespace NorthwindCorp.Web.Controllers.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriesController : ControllerBase
  {
    private readonly ICategoryService _categoryService;
    private readonly ILogger<Controllers.CategoriesController> _logger;

    public CategoriesController(ICategoryService categoryService, ILogger<Controllers.CategoriesController> logger)
    {
      _logger = logger;
      _categoryService = categoryService;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
      var result = (await _categoryService.GetCategoriesAsync()).Select(c => new CategoryDto()
      {
        CategoryId = c.CategoryId,
        CategoryName = c.CategoryName,
        Description = c.Description
      });

      return Ok(result);
    }
  }
}
