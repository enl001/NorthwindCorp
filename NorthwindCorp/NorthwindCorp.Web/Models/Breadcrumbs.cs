using System;

namespace NorthwindCorp.Web.Models
{
  public class Breadcrumbs
  {
    public string Controller { get; set; } = String.Empty;
    public string Action { get; set; } = String.Empty;
    public string Page { get; set; } = String.Empty;
    public string Area { get; set; } = String.Empty;
  }
}
