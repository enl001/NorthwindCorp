using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindCorp.Core.Helpers;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using NorthwindCorp.Web.Models;

namespace NorthwindCorp.Web.Controllers
{
  [Authorize]
  [Route("[controller]")]
  public class CategoriesController : Controller
  {
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
    {
      _logger = logger;
      _categoryService = categoryService;
    }

    [AllowAnonymous]
    [HttpGet()]
    [ProducesResponseType(200)]
    public IActionResult Index()
    {
      var categoryList = _categoryService.GetCategories().ToList();
      return View(categoryList);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}/image")]
    [ProducesResponseType(200)]
    public IActionResult GetImage(int id)
    {
      var category = _categoryService.GetCategoryById(id);
      if (category == null)
      {
        _logger.LogError($"Can't get image with id={id}");
        return RedirectToAction("Error", "Home");
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

    [HttpGet("{id:int}/upload-image", Name = "UploadImage")]
    [ProducesResponseType(200)]
    public IActionResult UploadImage(int id)
    {
      return View(new UploadImage { });
    }

    [HttpPost("{id:int}/upload-image", Name = "UploadImage")]
    [ProducesResponseType(200)]
    public IActionResult UploadImage(UploadImage uploadedImage, [FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return View(uploadedImage);
      }

      var image = uploadedImage.Image;
      byte[] imageData = null;
      using (var binaryReader = new BinaryReader(image.OpenReadStream()))
      {
        imageData = binaryReader.ReadBytes((int)image.Length);
      }

      var imageForDb = ImageHelper.AddOleHeader(imageData);

      var category = _categoryService.GetCategoryById(id);
      category.Picture = imageForDb;

      var result = _categoryService.UpdateCategory(category);

      if (result == false)
      {
        return RedirectToAction("Error", "Home");
      }

      return RedirectToAction("Index");
    }
  }
}
