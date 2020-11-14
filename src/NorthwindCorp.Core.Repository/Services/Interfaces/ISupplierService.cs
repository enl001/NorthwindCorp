using NorthwindCorp.Core.Repository.Models;
using System.Collections.Generic;

namespace NorthwindCorp.Core.Repository.Services.Interfaces
{
  public interface ISupplierService
  {
    IEnumerable<Supplier> GetSuppliers();
  }
}
