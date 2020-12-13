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
      string actionName = String.Empty;
      string controller = String.Empty;
      string page = String.Empty;
      string area = String.Empty;
      if (ViewContext.RouteData.Values.ContainsKey("action"))
      {
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

        controller = ViewContext.RouteData.Values["controller"].ToString();
      }
      if (ViewContext.RouteData.Values.ContainsKey("page"))
      {
        page = ViewContext.RouteData.Values["page"].ToString();
        area = ViewContext.RouteData.Values["area"].ToString();
      }

      var breadcrumbs = new Breadcrumbs()
      {
        Controller = controller,
        Action = actionName,
        Page = page,
        Area = area
      };
      return View(breadcrumbs);
    }
  }
}
