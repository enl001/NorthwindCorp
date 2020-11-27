using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindCorp.Web.Models;

namespace NorthwindCorp.Web.ViewComponents
{
  [ViewComponent]
  public class BreadcrumbsViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke()
    {
      var dd = ViewContext.RouteData.Routers;
      string actionName;  
      switch (ViewContext.RouteData.Values["action"].ToString())
      {
        case "UploadImage":
          actionName = "Upload Image";
          break;
        case "UpdateProduct":
          actionName = "Update Product";
          break;
        case "CreateNewProduct":
          actionName = "Create New Product";
          break;
        default:
          actionName = ViewContext.RouteData.Values["action"].ToString();
          break;
      }

      var breadcrumbs = new Breadcrumbs()
      {
        Controller = ViewContext.RouteData.Values["controller"].ToString(),
        Action = actionName
      };
      return View(breadcrumbs);
    }
  }
}
