using NorthwindCorp.Core.Repository.Data;
using NorthwindCorp.Core.Repository.Models;
using System.Collections.Generic;
using System.Linq;
using NorthwindCorp.Core.Repository.Services.Interfaces;


namespace NorthwindCorp.Core.Repository.Services
{
  public class SupplierService : ISupplierService
  {
    private readonly NorthwindContext _northwindContext;

    public SupplierService(NorthwindContext northwindDataContext)
    {
      _northwindContext = northwindDataContext;
    }

    public IEnumerable<Supplier> GetSuppliers()
    {
      return _northwindContext.Suppliers.ToList();
    }
  }
}
