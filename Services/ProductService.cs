using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NorthwindCorp.Data;
using NorthwindCorp.DTO;
using NorthwindCorp.Models;

namespace NorthwindCorp.Services
{
  public class ProductService
  {
    private readonly NorthwindDataContext _northwindDataContext;
    private readonly IConfiguration _configuration;
    private readonly CategoryService _categoryService;
    private readonly SupplierService _supplierService;
    private readonly ConfigurationService _configurationService;

    public ProductService(
      NorthwindDataContext northwindDataContext,
      IConfiguration configuration,
      CategoryService categoryService,
      SupplierService supplierService,
      ConfigurationService configurationService)
    {
      _configurationService = configurationService;
      _supplierService = supplierService;
      _categoryService = categoryService;
      _configuration = configuration;
      _northwindDataContext = northwindDataContext;
    }

    public IEnumerable<Product> GetAllProducts()
    {
      var products = _configurationService.GetValue<int>("AmountOfProductsToShow") > 0
        ? (from p in _northwindDataContext.Products
           join c in _northwindDataContext.Categories
             on p.CategoryID equals c.CategoryId
           join s in _northwindDataContext.Suppliers
             on p.SupplierID equals s.SupplierID
           orderby p.ProductID descending
           select new Product
           {
             Id = p.ProductID,
             Name = p.ProductName,
             SupplierId = p.SupplierID,
             SupplierName = s.CompanyName,
             CategoryId = c.CategoryId,
             CategoryName = c.CategoryName,
             QuantityPerUnit = p.QuantityPerUnit,
             UnitPrice = p.UnitPrice,
             UnitsInStock = p.UnitsInStock,
             UnitsOnOrder = p.UnitsOnOrder,
             ReorderLevel = p.ReorderLevel,
             Discontinued = p.Discontinued
           }).Take(_configurationService.GetValue<int>("AmountOfProductsToShow")).ToList()
        : (from p in _northwindDataContext.Products
           join c in _northwindDataContext.Categories
             on p.CategoryID equals c.CategoryId
           join s in _northwindDataContext.Suppliers
             on p.SupplierID equals s.SupplierID
           orderby p.ProductID descending
           select new Product
           {
             Id = p.ProductID,
             Name = p.ProductName,
             SupplierId = p.SupplierID,
             SupplierName = s.CompanyName,
             CategoryId = c.CategoryId,
             CategoryName = c.CategoryName,
             QuantityPerUnit = p.QuantityPerUnit,
             UnitPrice = p.UnitPrice,
             UnitsInStock = p.UnitsInStock,
             UnitsOnOrder = p.UnitsOnOrder,
             ReorderLevel = p.ReorderLevel,
             Discontinued = p.Discontinued
           }).ToList();

      return products;
    }

    public Product GetProductById(int id)
    {
      var products = (from p in _northwindDataContext.Products
                     join c in _northwindDataContext.Categories
                       on p.CategoryID equals c.CategoryId
                     join s in _northwindDataContext.Suppliers
                       on p.SupplierID equals s.SupplierID
                     where p.ProductID == id
                     select new Product
                     {
                       Id = p.ProductID,
                       Name = p.ProductName,
                       SupplierId = p.SupplierID,
                       SupplierName = s.CompanyName,
                       CategoryId = c.CategoryId,
                       CategoryName = c.CategoryName,
                       QuantityPerUnit = p.QuantityPerUnit,
                       UnitPrice = p.UnitPrice,
                       UnitsInStock = p.UnitsInStock,
                       UnitsOnOrder = p.UnitsOnOrder,
                       ReorderLevel = p.ReorderLevel,
                       Discontinued = p.Discontinued
                     }).ToList();


      return (products.Any())
        ? products[0]
        : null;
    }

    public bool CreateNewProduct(CreateProductModel createProductModel)
    {
      ProductDto product = new ProductDto
      {
        ProductID = createProductModel.Product.Id,
        ProductName = createProductModel.Product.Name,
        SupplierID = createProductModel.Product.SupplierId,
        CategoryID = createProductModel.Product.CategoryId,
        QuantityPerUnit = createProductModel.Product.QuantityPerUnit,
        UnitPrice = createProductModel.Product.UnitPrice,
        UnitsInStock = createProductModel.Product.UnitsInStock,
        UnitsOnOrder = createProductModel.Product.UnitsOnOrder,
        ReorderLevel = createProductModel.Product.ReorderLevel,
        Discontinued = createProductModel.Product.Discontinued
      };

      _northwindDataContext.Products.Add(product);
      var result = _northwindDataContext.SaveChanges();
      return result > 0;
    }

    public bool UpdateProduct(CreateProductModel updateProduct, int id)
    {
      ProductDto product = new ProductDto
      {
        ProductID = id,
        ProductName = updateProduct.Product.Name,
        SupplierID = updateProduct.Product.SupplierId,
        CategoryID = updateProduct.Product.CategoryId,
        QuantityPerUnit = updateProduct.Product.QuantityPerUnit,
        UnitPrice = updateProduct.Product.UnitPrice,
        UnitsInStock = updateProduct.Product.UnitsInStock,
        UnitsOnOrder = updateProduct.Product.UnitsOnOrder,
        ReorderLevel = updateProduct.Product.ReorderLevel,
        Discontinued = updateProduct.Product.Discontinued
      };

      _northwindDataContext.Products.Update(product);
      var result = _northwindDataContext.SaveChanges();
      return result > 0;
    }

    public CreateProductModel CreateNewProductModel()
    {
      var supplierList = _supplierService.GetSelectListSuppliers();

      var categoryList = _categoryService.GetSelectListCategories();
      
      return new CreateProductModel
      {
        Categories = categoryList,
        Suppliers = supplierList
      };
    }

    public CreateProductModel UpdateProductModel(CreateProductModel productModel)
    {
      var supplierList = _supplierService.GetSelectListSuppliers();

      var categoryList = _categoryService.GetSelectListCategories();

      productModel.Suppliers = supplierList;
      productModel.Categories = categoryList;
      return productModel;
    }
  }
}
