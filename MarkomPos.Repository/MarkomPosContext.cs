using MarkomPoss.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository
{
    public class MarkomPosContext : DbContext
    {
        public MarkomPosContext(DbContextOptions<MarkomPosContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductInfo> ProductInfoes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
