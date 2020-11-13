using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindCorp.Data;

namespace NorthwindCorp.Services
{
  public class SupplierService
  {
    private NorthwindDataContext _northwindDataContext;

    public SupplierService(NorthwindDataContext northwindDataContext)
    {
      _northwindDataContext = northwindDataContext;
    }
    
    public IEnumerable<SelectListItem> GetSelectListSuppliers()
    {
      return _northwindDataContext.Suppliers.Select(supplier => new SelectListItem
      {
        Text = supplier.CompanyName,
        Value = supplier.SupplierID.ToString()
      }).ToList();
    }
  }
}
