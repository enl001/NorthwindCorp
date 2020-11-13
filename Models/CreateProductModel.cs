using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthwindCorp.Models
{
  public class CreateProductModel
  {
    public Product Product { get; set; }    
    public IEnumerable<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
  }
}
