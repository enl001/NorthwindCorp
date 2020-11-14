using NorthwindCorp.Core.Repository.Data;
using NorthwindCorp.Core.Repository.Models;
using System.Collections.Generic;
using System.Linq;
using NorthwindCorp.Core.Repository.Services.Interfaces;


namespace NorthwindCorp.Core.Repository.Services
{
  public class SupplierService : ISupplierService
  {
    private readonly NorthwindContext _northwindDataContext;

    public SupplierService(NorthwindContext northwindDataContext)
    {
      _northwindDataContext = northwindDataContext;
    }

    public IEnumerable<Supplier> GetSuppliers()
    {
      return _northwindDataContext.Suppliers.ToList();
    }
  }
}
