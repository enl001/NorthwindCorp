using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindCorp.Models;
using NorthwindCorp.Services;

namespace NorthwindCorp.Controllers
{
  [Route("products")]
  public class ProductController : Controller
  {
    private ProductService _productService;

    public ProductController(ProductService productService)
    {
      _productService = productService;
    }

    [HttpGet("all")]
    [ProducesResponseType(200)]
    public IActionResult GetAllProducts()
    {
      var productList = _productService.GetAllProducts();
      return View(productList);
    }

    [HttpGet("product/{id}")]
    [ProducesResponseType(200)]
    public IActionResult GetProductById(int id)
    {
      var product = _productService.GetProductById(id);
      return View(product);
    }

    [HttpGet("create")]
    [ProducesResponseType(200)]
    public IActionResult CreateNewProduct()
    {
      var productModel =_productService.CreateNewProductModel();

      return View(productModel);
    }

    [HttpPost("create")]
    [ProducesResponseType(200)]
    public IActionResult CreateNewProduct(CreateProductModel createdProduct)
    {
      if (!ModelState.IsValid)
      {
        return View(_productService.UpdateProductModel(createdProduct));
      }
      _productService.CreateNewProduct(createdProduct);
      return RedirectToAction("GetAllProducts");
    }
    
  }
}
