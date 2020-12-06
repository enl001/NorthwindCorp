using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindCorp.Core.DTO;
using NorthwindCorp.Core.Repository.Models;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindCorp.Web.Controllers.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly ISupplierService _supplierService;
    private ILogger<Controllers.CategoriesController> _logger;

    public ProductsController(
      ICategoryService categoryService,
      IProductService productService,
      ISupplierService supplierService,
      ILogger<Controllers.CategoriesController> logger)
    {
      _logger = logger;
      _categoryService = categoryService;
      _productService = productService;
      _supplierService = supplierService;
    }

    /// <summary>
    /// Retrieves all products
    /// </summary>
    /// <response code="200">Products retrieved</response>
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
      var result = (await _productService.GetProductsAsync()).Select(p => new ProductDto()
      {
        ProductId = p.ProductId,
        ProductName = p.ProductName,
        QuantityPerUnit = p.QuantityPerUnit,
        UnitPrice = p.UnitPrice,
        UnitsInStock = p.UnitsInStock,
        UnitsOnOrder = p.UnitsOnOrder,
        ReorderLevel = p.ReorderLevel,
        Discontinued = p.Discontinued,
        Category = new CategoryDto()
        {
          CategoryId = p.Category.CategoryId,
          CategoryName = p.Category.CategoryName,
          Description = p.Category.Description
        },
        Supplier = new SupplierDto()
        {
          SupplierId = p.Supplier.SupplierId,
          CompanyName = p.Supplier.CompanyName,
          ContactName = p.Supplier.ContactName
        }
      });

      return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific product by unique id
    /// </summary>
    /// <param name="id" example="1">The product id</param>
    /// <response code="200">Product retrieved</response>
    /// <response code="400">Product has invalid id</response>
    /// <response code="404">Product has invalid id</response>
    [HttpGet("{id}", Name = "GetProductById")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Product>> GetProductById([FromRoute] int id)
    {
      try
      {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
          return NotFound();
        }
        return Ok(product);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
      }

      return BadRequest();
    }

    /// <summary>
    /// Create product
    /// </summary>
    /// <param name="productToCreate">The product</param>
    /// <response code="201">Product created</response>
    /// <response code="400">Product has missing/invalid values</response>
    [HttpPost("", Name = "CreateProduct")]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product productToCreate)
    {
      try
      {
        var product = await _productService.CreateProductAsync(productToCreate);
        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);

      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
      }

      return BadRequest();
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="productToUpdate">The product</param>
    /// <param name="id">The product id</param>
    /// <response code="204">Product updated</response>
    /// <response code="404">No product with such id</response>
    [HttpPut("{id}", Name = "PutProduct")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Product>> PutProduct([FromRoute] int id, [FromBody] Product productToUpdate)
    {
      try
      {
        
        if(!await _productService.ProductIsExists(id))
        {
          return NotFound();
        }
        productToUpdate.ProductId = id;
        await _productService.UpdateProductAsync(productToUpdate);

      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
          return NotFound();
        }
      }

      return NoContent();
    }

    /// <summary>
    /// Delete product by id
    /// </summary>
    /// <param name="id" example="1">The product id</param>
    /// <response code="200">Product deleted</response>
    /// <response code="400">Something goes wrong</response>
    /// <response code="404">Product not found</response>
    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Product>> DeleteProduct([FromRoute] int id)
    {
      try
      {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
          return NotFound();
        }
        await _productService.DeleteProductAsync(product);
        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
      }

      return BadRequest();
    }
  }
}
