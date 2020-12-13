using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindCorp.Core.DTO;
using NorthwindCorp.Core.Helpers;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

    /// <summary>
    /// Retrieves all categories
    /// </summary>
    /// <response code="200">Categories retrieved</response>
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

    /// <summary>
    /// Retrieves a specific category by unique id
    /// </summary>
    /// <param name="id" example="1">The category id</param>
    /// <response code="200">Category retrieved</response>
    /// <response code="400">Category has invalid id</response>
    /// <response code="404">Category has invalid id</response>
    [HttpGet("{id:int}/image", Name = "GetImageFile")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetImageFile([FromRoute] int id)
    {
      var category = await _categoryService.GetCategoryByIdAsync(id);
      if (category == null)
      {
        return NotFound();
      }
      if (category.Picture == null)
      {
        category.Picture = new byte[] { };
      }
      using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
      {
        // 78 is the size of the OLE header for Northwind images
        if (category.Picture.Length > 78)
        {
          ms.Write(category.Picture, 78, category.Picture.Length - 78);
          return File(ms.ToArray(), "image/jpg");
        }

        return File(category.Picture, "image/jpg");
      }
    }

    /// <summary>
    /// Update category image
    /// </summary>
    /// <param name="image">The image</param>
    /// <param name="id">The category id</param>
    /// <response code="204">Category updated</response>
    /// <response code="400">Something goes wrong</response>
    /// <response code="404">No category with such id</response>
    [HttpPut("{id:int}/image", Name = "UpdateImage")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateImage([FromForm] IFormFile image, [FromRoute] int id)
    {
      byte[] imageData = null;

      if (image.Length == 0)
      {
        return BadRequest();
      }

      if (!await _categoryService.CategoryIsExistsAsync(id))
      {
        return NotFound();
      }

      using (var binaryReader = new BinaryReader(image.OpenReadStream()))
      {
        imageData = binaryReader.ReadBytes((int)image.Length);
      }

      var imageForDb = ImageHelper.AddOleHeader(imageData);

      try
      {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        category.Picture = imageForDb;
        await _categoryService.UpdateCategoryAsync(category);
        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
          return NotFound();
        }
      }

      return BadRequest();
    }
  }
}
