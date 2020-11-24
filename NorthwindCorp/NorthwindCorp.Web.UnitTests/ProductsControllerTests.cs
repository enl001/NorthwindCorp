using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindCorp.Core.Repository.Models;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using NorthwindCorp.Web.Controllers;
using NorthwindCorp.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NorthwindCorp.Web.UnitTests
{
  public class ProductsControllerTests
  {

    [Fact]
    public void Index_ReturnsAViewResult()
    {
      // Arrange
      List<Product> products = new List<Product>
      {
        new Product
        {
          ProductName = "Name",
          SupplierId = 1,
          CategoryId = 1,
          QuantityPerUnit ="10",
          UnitPrice =5,
          UnitsInStock =5,
          UnitsOnOrder =2,
          ReorderLevel =1,
          Discontinued = true,
          Category = new Category()
          {
            CategoryId = 1,
            CategoryName = "TestName1",
            Description = "TestDescription1",
            Picture = new byte[] { }
          },
          Supplier = new Supplier()
          {
            CompanyName ="Test company"
          }
        },
        new Product
        {
          ProductName = "Name",
          SupplierId = 1,
          CategoryId = 1,
          QuantityPerUnit ="10",
          UnitPrice =5,
          UnitsInStock =5,
          UnitsOnOrder =2,
          ReorderLevel =1,
          Discontinued = true,
          Category = new Category()
          {
            CategoryId = 1,
            CategoryName = "TestName1",
            Description = "TestDescription1",
            Picture = new byte[] { }
          },
          Supplier = new Supplier() {
            CompanyName ="Test company"
          }
        },
        new Product
        {
          ProductName = "Name",
          SupplierId = 1,
          CategoryId = 1,
          QuantityPerUnit ="10",
          UnitPrice =5,
          UnitsInStock =5,
          UnitsOnOrder =2,
          ReorderLevel =1,
          Discontinued = true,
          Category = new Category()
          {
            CategoryId = 1,
            CategoryName = "TestName1",
            Description = "TestDescription1",
            Picture = new byte[] { }
          },
          Supplier = new Supplier() {
            CompanyName ="Test company"
          }
        }
      };
      var loggerMock = Mock.Of<ILogger<ProductsController>>();
      var productServiceMock = new Mock<IProductService>();
      var categoryServiceMock = new Mock<ICategoryService>();
      var supplierServiceMock = new Mock<ISupplierService>();
      var configurationService = new Mock<IConfigurationService>();
      configurationService.Setup(repo => repo.GetValue<int>("AmountOfProductsToShow")).Returns(2);

      productServiceMock.Setup(repo => repo.GetProducts()).Returns(products.AsQueryable());

      var controller = new ProductsController(
        productServiceMock.Object,
        supplierServiceMock.Object,
        categoryServiceMock.Object,
        configurationService.Object,
        loggerMock);

      // Act
      var result = controller.Index();

      // Assert
      var viewResult = Assert.IsType<ViewResult>(result);
      var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.ViewData.Model);
      Assert.Equal(2, model.Count());
    }

    [Fact]
    public void GetProductById_ReturnsAViewResult()
    {
      // Arrange

      var product = new Product
      {
        ProductName = "Name",
        SupplierId = 1,
        CategoryId = 1,
        QuantityPerUnit = "10",
        UnitPrice = 5,
        UnitsInStock = 5,
        UnitsOnOrder = 2,
        ReorderLevel = 1,
        Discontinued = true,
        Category = new Category()
        {
          CategoryId = 1,
          CategoryName = "TestName1",
          Description = "TestDescription1",
          Picture = new byte[] { }
        },
        Supplier = new Supplier()
        {
          CompanyName = "Test company"
        }
      };
       
      var loggerMock = Mock.Of<ILogger<ProductsController>>();
      var productServiceMock = new Mock<IProductService>();
      var categoryServiceMock = new Mock<ICategoryService>();
      var supplierServiceMock = new Mock<ISupplierService>();
      var configurationService = new Mock<IConfigurationService>();
     
      productServiceMock.Setup(repo => repo.GetProductById(1)).Returns(product);

      var controller = new ProductsController(
        productServiceMock.Object,
        supplierServiceMock.Object,
        categoryServiceMock.Object,
        configurationService.Object,
        loggerMock);

      // Act
      var result = controller.GetProductById(1);

      // Assert
      var viewResult = Assert.IsType<ViewResult>(result);
      var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
      Assert.Equal(product, model);
    }

    [Fact]
    public void Get_CreateNewProduct_ReturnsAViewResult()
    {
      // Arrange
      var categories = new List<Category>()
      {
        new Category
        {
          CategoryId = 1,
          CategoryName = "TestName1",
        },
        new Category
        {
          CategoryId = 2,
          CategoryName = "TestName2",
        }
      };
      var suppliers = new List<Supplier>()
      {
        new Supplier
        {
          SupplierId = 1,
          CompanyName = "TestCompanyName1",
        },
        new Supplier
        {
          SupplierId = 2,
          CompanyName = "TestCompanyName2",
        }
      };

      var loggerMock = Mock.Of<ILogger<ProductsController>>();
      var productServiceMock = new Mock<IProductService>();
      var categoryServiceMock = new Mock<ICategoryService>();
      var supplierServiceMock = new Mock<ISupplierService>();
      var configurationService = new Mock<IConfigurationService>();

      categoryServiceMock.Setup(repo => repo.GetCategories()).Returns(categories.AsQueryable());
      supplierServiceMock.Setup(repo => repo.GetSuppliers()).Returns(suppliers.AsQueryable());

      var controller = new ProductsController(
        productServiceMock.Object,
        supplierServiceMock.Object,
        categoryServiceMock.Object,
        configurationService.Object,
        loggerMock);

      // Act
      var result = controller.CreateNewProduct();

      // Assert
      var viewResult = Assert.IsType<ViewResult>(result);
      var model = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
      Assert.Equal(2, model.Suppliers.Count());
      Assert.Equal(2, model.Categories.Count());
    }

    [Fact]
    public void Put_CreateNewProduct_ReturnsRedirect()
    {
      // Arrange
      var categories = new List<Category>()
      {
        new Category
        {
          CategoryId = 1,
          CategoryName = "TestName1",
        },
        new Category
        {
          CategoryId = 2,
          CategoryName = "TestName2",
        }
      };
      var suppliers = new List<Supplier>()
      {
        new Supplier
        {
          SupplierId = 1,
          CompanyName = "TestCompanyName1",
        },
        new Supplier
        {
          SupplierId = 2,
          CompanyName = "TestCompanyName2",
        }
      };

      var loggerMock = Mock.Of<ILogger<ProductsController>>();
      var productServiceMock = new Mock<IProductService>();
      var categoryServiceMock = new Mock<ICategoryService>();
      var supplierServiceMock = new Mock<ISupplierService>();
      var configurationService = new Mock<IConfigurationService>();

      categoryServiceMock.Setup(repo => repo.GetCategories()).Returns(categories.AsQueryable());
      supplierServiceMock.Setup(repo => repo.GetSuppliers()).Returns(suppliers.AsQueryable());
      productServiceMock.Setup(repo => repo.CreateProduct(It.IsAny<Product>())).Returns(true);

      var controller = new ProductsController(
        productServiceMock.Object,
        supplierServiceMock.Object,
        categoryServiceMock.Object,
        configurationService.Object,
        loggerMock);

      // Act
      var result = controller.CreateNewProduct(new Product());

      // Assert
      var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
      Assert.Equal("Index", redirectToActionResult.ActionName);
    }

    [Fact]
    public void Get_UpdateProduct_ReturnsARedirectToError()
    {
      // Arrange
      var categories = new List<Category>()
      {
        new Category
        {
          CategoryId = 1,
          CategoryName = "TestName1",
        },
        new Category
        {
          CategoryId = 2,
          CategoryName = "TestName2",
        }
      };
      var suppliers = new List<Supplier>()
      {
        new Supplier
        {
          SupplierId = 1,
          CompanyName = "TestCompanyName1",
        },
        new Supplier
        {
          SupplierId = 2,
          CompanyName = "TestCompanyName2",
        }
      };      

      var loggerMock = Mock.Of<ILogger<ProductsController>>();
      var productServiceMock = new Mock<IProductService>();
      var categoryServiceMock = new Mock<ICategoryService>();
      var supplierServiceMock = new Mock<ISupplierService>();
      var configurationService = new Mock<IConfigurationService>();

      categoryServiceMock.Setup(repo => repo.GetCategories()).Returns(categories.AsQueryable());
      supplierServiceMock.Setup(repo => repo.GetSuppliers()).Returns(suppliers.AsQueryable());
      productServiceMock.Setup(repo => repo.GetProductById(1)).Returns<Product>(null);

      var controller = new ProductsController(
        productServiceMock.Object,
        supplierServiceMock.Object,
        categoryServiceMock.Object,
        configurationService.Object,
        loggerMock);

      // Act
      var result = controller.UpdateProduct(1);

      // Assert
      var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
      Assert.Equal("Home", redirectToActionResult.ControllerName);
      Assert.Equal("Error", redirectToActionResult.ActionName);
    }

    [Fact]
    public void Put_UpdateProduct_ModelIsInvalid_ReturnsAViewResult()
    {
      // Arrange
      var categories = new List<Category>()
      {
        new Category
        {
          CategoryId = 1,
          CategoryName = "TestName1",
        },
        new Category
        {
          CategoryId = 2,
          CategoryName = "TestName2",
        }
      };
      var suppliers = new List<Supplier>()
      {
        new Supplier
        {
          SupplierId = 1,
          CompanyName = "TestCompanyName1",
        },
        new Supplier
        {
          SupplierId = 2,
          CompanyName = "TestCompanyName2",
        }
      };

      var loggerMock = Mock.Of<ILogger<ProductsController>>();
      var productServiceMock = new Mock<IProductService>();
      var categoryServiceMock = new Mock<ICategoryService>();
      var supplierServiceMock = new Mock<ISupplierService>();
      var configurationService = new Mock<IConfigurationService>();

      categoryServiceMock.Setup(repo => repo.GetCategories()).Returns(categories.AsQueryable());
      supplierServiceMock.Setup(repo => repo.GetSuppliers()).Returns(suppliers.AsQueryable());
      productServiceMock.Setup(repo => repo.GetProductById(1)).Returns<Product>(null);

      var controller = new ProductsController(
        productServiceMock.Object,
        supplierServiceMock.Object,
        categoryServiceMock.Object,
        configurationService.Object,
        loggerMock);

      controller.ModelState.AddModelError("ProductName", "Required");

      // Act
      var result = controller.UpdateProduct(new Product(), 1);

      // Assert
      var viewResult = Assert.IsType<ViewResult>(result);
    }
  }
}
