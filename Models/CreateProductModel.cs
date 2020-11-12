using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
