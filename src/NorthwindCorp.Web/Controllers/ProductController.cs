using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using NorthwindCorp.Models;
using NorthwindCorp.Services;

namespace NorthwindCorp.Controllers
{
  [Route("products")]
  public class ProductController : Controller
  {
    private ProductService _productService;
    private ILogger<ProductController> _logger;

    public ProductController(ProductService productService, ILogger<ProductController> logger)
    {
      _logger = logger;
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

      if (product == null)
      {
        _logger.LogError($"Can't get product with id={id}");
        return RedirectToAction("Error", "Home");
      }
      return View(product);

    }

    [HttpGet("create")]
    [ProducesResponseType(200)]
    public IActionResult CreateNewProduct()
    {
      var productModel = _productService.CreateNewProductModel();

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
      
      var result = _productService.CreateNewProduct(createdProduct);

      if (result == false)
      {
        _logger.LogError($"Creating new product fails");
        return RedirectToAction("Error", "Home");
      }

      return RedirectToAction("GetAllProducts");
    }

    [HttpGet("update-product/{id}")]
    [ProducesResponseType(200)]
    public IActionResult UpdateProduct(int id)
    {
      var product = _productService.GetProductById(id);

      if (product == null)
      {
        _logger.LogError($"Can't update product with id={id}");
        return RedirectToAction("Error", "Home");
      }

      product.Id = id;
      var updateProduct = new CreateProductModel
      {
        Product = product
      };

      return View(_productService.UpdateProductModel(updateProduct));
    }

    [HttpPost("update-product/{id}")]
    [ProducesResponseType(200)]
    public IActionResult UpdateProduct(CreateProductModel updateProduct)
    {
      if (!ModelState.IsValid)
      {
        return View(_productService.UpdateProductModel(updateProduct));
      }
      
      if (!int.TryParse(HttpContext.GetRouteValue("id")?.ToString(), out int id))
      {
        _logger.LogError($"Can't retrieve product id");
        return RedirectToAction("Error", "Home");
      }
      
      var result = _productService.UpdateProduct(updateProduct, id);

      if (result == false)
      {
        _logger.LogError($"Updating product fails");
        return RedirectToAction("Error", "Home");
      }

      return RedirectToAction("GetAllProducts");
    }
  }
}
