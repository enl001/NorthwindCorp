using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    private CategoryService _categoryService;
    private SupplierService _supplierService;

    public ProductService(
      NorthwindDataContext northwindDataContext,
      IConfiguration configuration,
      CategoryService categoryService,
      SupplierService supplierService)
    {
      _supplierService = supplierService;
      _categoryService = categoryService;
      _configuration = configuration;
      _northwindDataContext = northwindDataContext;
    }

    public IEnumerable<Product> GetAllProducts()
    {
      var products = _configuration.GetValue<int>("AmountOfProductsToShow") > 0
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
           }).Take(_configuration.GetValue<int>("AmountOfProductsToShow")).ToList()
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
      var product = (from p in _northwindDataContext.Products
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
                     });

      return product.FirstOrDefault(null);
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
      var res = _northwindDataContext.Products.Add(product);
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
