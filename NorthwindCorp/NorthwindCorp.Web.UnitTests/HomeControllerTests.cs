using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindCorp.Web.Controllers;
using Xunit;

namespace NorthwindCorp.Web.UnitTests
{
  public class HomeControllerTests
  {
    [Fact]
    public void Index_ReturnsAViewResult()
    {
      // Arrange
      var logger = Mock.Of<ILogger<HomeController>>();
      var controller = new HomeController(logger);

      // Act
      var result = controller.Index();

      // Assert
      Assert.IsType<ViewResult>(result);
    }
  }
}
