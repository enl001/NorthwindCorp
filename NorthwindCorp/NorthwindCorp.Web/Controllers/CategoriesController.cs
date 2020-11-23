using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using NorthwindCorp.Web.Models;

namespace NorthwindCorp.Web.Controllers
{
  [Route("[controller]")]
  public class CategoriesController : Controller
  {
    private readonly ICategoryService _categoryService;
    private ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
    {
      _logger = logger;
      _categoryService = categoryService;
    }

    [HttpGet()]
    [ProducesResponseType(200)]
    public IActionResult Index()
    {
      var categoryList = _categoryService.GetCategories().ToList();
      return View(categoryList);
    }
    
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

      var imageForDb = this.AddOleHeader(imageData);

      var category = _categoryService.GetCategoryById(id);
      category.Picture = imageForDb;

      var result = _categoryService.UpdateCategory(category);

      if (result == false)
      {
        return RedirectToAction("Error", "Home");
      }

      return RedirectToAction("Index");
    }

    private byte[] AddOleHeader(byte[] data)
    {
      string oleString = "000101010001110000101111000000000000001000000000000000000000000000001101000000000000111000000000000101000000000000100001000000001111111111111111111111111111111101000010011010010111010001101101011000010111000000100000010010010110110101100001011001110110010100000000010100000110000101101001011011100111010000101110010100000110100101100011011101000111010101110010011001010000000000000001000001010000000000000000000000100000000000000000000000000000011100000000000000000000000001010000010000100111001001110101011100110110100000000000000000000000000000000000000000000000000000000000000000000000000010100000001010010000000000000000";
      var ole = this.GetOle(oleString);
      byte[] newData = new byte[data.Length + 78];
      System.Buffer.BlockCopy(ole, 0, newData, 0, ole.Length);
      System.Buffer.BlockCopy(data, 0, newData, ole.Length, data.Length);
      return newData;
    }

    private byte[] GetOle(string input)
    {
      int numOfBytes = input.Length / 8;
      byte[] bytes = new byte[numOfBytes];
      for (int i = 0; i < numOfBytes; ++i)
      {
        bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
      }
      return bytes;
    }
  }
}
