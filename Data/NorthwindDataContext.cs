﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthwindCorp.DTO;


namespace NorthwindCorp.Data
{
  public class NorthwindDataContext : DbContext
  {
    public DbSet<CategoryDto> Categories { get; set; }
    public DbSet<ProductDto> Products { get; set; }
    public DbSet<SupplierDto> Suppliers { get; set; }
    public NorthwindDataContext(DbContextOptions<NorthwindDataContext> options) : base(options)  
    {
      Database.EnsureCreated();
    }
  }
}
