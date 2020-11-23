using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindCorp.Core.Repository.Models;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using NorthwindCorp.Web.Controllers;
using NorthwindCorp.Web.Models;
using Xunit;

namespace NorthwindCorp.Web.UnitTests
{
  public class CategoriesControllerTests
  {

    [Fact]
    public void Index_ReturnsAViewResult()
    {
      // Arrange
      List<Category> categories = new List<Category>
      {
        new Category
        {
          CategoryId = 1,
          CategoryName = "TestName1",
          Description = "TestDescription1",
          Picture = new byte[] {}
        },
        new Category
        {
          CategoryId = 2,
          CategoryName = "TestName2",
          Description = "TestDescription2",
          Picture = new byte[] {}
        }
      };
      var loggerMock = Mock.Of<ILogger<CategoriesController>>();
      var categoryServiceMock = new Mock<ICategoryService>();
      categoryServiceMock.Setup(repo => repo.GetCategories()).Returns(categories.AsQueryable());
      var controller = new CategoriesController(categoryServiceMock.Object, loggerMock);

      // Act
      var result = controller.Index();

      // Assert
      var viewResult = Assert.IsType<ViewResult>(result);
      var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.ViewData.Model);
      Assert.Equal(2, model.Count());
    }

    [Fact]
    public void GetImage_ReturnsAFile()
    {
      // Arrange
      var category =
        new Category
        {
          CategoryId = 1,
          CategoryName = "TestName1",
          Description = "TestDescription1",
          Picture = new byte[] { 0x20 }
        };

      var loggerMock = Mock.Of<ILogger<CategoriesController>>();
      var categoryServiceMock = new Mock<ICategoryService>();
      categoryServiceMock.Setup(repo => repo.GetCategoryById(1)).Returns(category);
      var controller = new CategoriesController(categoryServiceMock.Object, loggerMock);

      // Act
      var result = controller.GetImage(1);

      // Assert
      var fileContentResult = Assert.IsType<FileContentResult>(result);
      Assert.Equal("image/jpg", fileContentResult.ContentType);
      Assert.Equal(category.Picture, fileContentResult.FileContents);
    }

    [Fact]
    public void GetImage_ReturnsRedirect()
    {
      // Arrange
      
      var loggerMock = Mock.Of<ILogger<CategoriesController>>();
      var categoryServiceMock = new Mock<ICategoryService>();
      categoryServiceMock.Setup(repo => repo.GetCategoryById(0)).Returns<Category>(null);
      var controller = new CategoriesController(categoryServiceMock.Object, loggerMock);

      // Act
      var result = controller.GetImage(1);

      // Assert
      var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
      Assert.Equal("Home", redirectToActionResult.ControllerName);
      Assert.Equal("Error", redirectToActionResult.ActionName);
    }

    [Fact]
    public void Get_UploadImage_ReturnsViewResult()
    {
      // Arrange
      var loggerMock = Mock.Of<ILogger<CategoriesController>>();
      var categoryServiceMock = new Mock<ICategoryService>();
      var controller = new CategoriesController(categoryServiceMock.Object, loggerMock);
      // Act
      var result = controller.UploadImage(1);

      // Assert
      var viewResult = Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Post_UploadImage_ReturnsRedirect()
    {
      // Arrange
      var category =
        new Category
        {
          CategoryId = 1,
          CategoryName = "TestName1",
          Description = "TestDescription1",
          Picture = new byte[] { 0x20 }
        };

      var loggerMock = Mock.Of<ILogger<CategoriesController>>();
      var categoryServiceMock = new Mock<ICategoryService>();
      categoryServiceMock.Setup(repo => repo.UpdateCategory(It.IsAny<Category>())).Returns(true);
      categoryServiceMock.Setup(repo => repo.GetCategoryById(1)).Returns(category);

      var controller = new CategoriesController(categoryServiceMock.Object, loggerMock);
      // Act
      IActionResult result;
      using (MemoryStream ms = new MemoryStream())
      {
        result = controller.UploadImage(new UploadImage() { Image = new FormFile(ms, 0, 0, "test", "test") }, 1);
      }
      // Assert
      var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
      Assert.Equal("Index", redirectToActionResult.ActionName);
    }
  }
}
