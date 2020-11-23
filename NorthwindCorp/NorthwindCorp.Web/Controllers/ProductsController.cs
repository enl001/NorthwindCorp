using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using NorthwindCorp.Core.Repository.Models;
using NorthwindCorp.Web.Services.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthwindCorp.Web.Controllers
{
  [Route("[controller]")]
  public class ProductsController : Controller
  {
    private readonly IProductService _productService;
    private readonly ISupplierService _supplierService;
    private readonly ICategoryService _categoryService;
    private readonly IConfigurationService _configurationService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(
      IProductService productService,
      ISupplierService supplierService,
      ICategoryService categoryService,
      IConfigurationService configurationService,
      ILogger<ProductsController> logger)
    {
      _logger = logger;
      _productService = productService;
      _supplierService = supplierService;
      _categoryService = categoryService;
      _configurationService = configurationService;
    }

    [HttpGet()]
    [ProducesResponseType(200)]
    public IActionResult Index()
    {
      var itemsToShow = _configurationService.GetValue<int>("AmountOfProductsToShow");
      var productList = (itemsToShow > 0)
        ? _productService.GetProducts().OrderByDescending(p => p.ProductId).Take(itemsToShow)
        : _productService.GetProducts();

      return View(productList);
    }

    [HttpGet("{id:int}")]
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
      var product = new Product();
      this.AddSelectListsToProduct(ref product);
      return View(product);
    }

    [HttpPost("create")]
    [ProducesResponseType(200)]
    public IActionResult CreateNewProduct(Product product)
    {
      if (!ModelState.IsValid)
      {
        this.AddSelectListsToProduct(ref product);
        return View(product);
      }

      var result = _productService.CreateProduct(product);

      if (result == false)
      {
        _logger.LogError($"Creating new product fails");
        return RedirectToAction("Error", "Home");
      }

      return RedirectToAction("Index");
    }

    [HttpGet("{id:int}/update")]
    [ProducesResponseType(200)]
    public IActionResult UpdateProduct(int id)
    {
      var product = _productService.GetProductById(id);

      if (product == null)
      {
        _logger.LogError($"Can't update product with id={id}");
        return RedirectToAction("Error", "Home");
      }

      this.AddSelectListsToProduct(ref product);

      return View(product);
    }

    [HttpPost("{id:int}/update", Name = "UpdateProduct")]
    [ProducesResponseType(200)]
    public IActionResult UpdateProduct(Product product, [FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        this.AddSelectListsToProduct(ref product);
        return View(product);
      }
      
      product.ProductId = id;
      var result = _productService.UpdateProduct(product);

      if (result == false)
      {
        _logger.LogError($"Updating product fails");
        return RedirectToAction("Error", "Home");
      }

      return RedirectToAction("Index");
    }

    private void AddSelectListsToProduct(ref Product product)
    {
      var categorySelectList = _categoryService.GetCategories().Select(category =>
      new SelectListItem
      {
        Text = category.CategoryName,
        Value = category.CategoryId.ToString()
      }).ToList();
      var supplierSelectList = _supplierService.GetSuppliers().Select(supplier =>
      new SelectListItem
      {
        Text = supplier.CompanyName,
        Value = supplier.SupplierId.ToString()
      }).ToList();

      product.Suppliers = supplierSelectList;
      product.Categories = categorySelectList;
    }

  }
}
