using Microsoft.EntityFrameworkCore;
using ProductsService.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsService.DataAccessLayer.Context;

public class ProductDbContext :DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) :base(options)    
    {        
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
