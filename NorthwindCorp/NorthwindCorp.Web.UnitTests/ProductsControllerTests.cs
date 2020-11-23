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

  }
}
