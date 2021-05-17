using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository
{
    public class markomPosDbContext : DbContext
    {
        public markomPosDbContext() : base("markonPosDbConn")
        {
        }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactStatus> ContactStatuses { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
