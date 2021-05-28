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
            //Database.SetInitializer(new dbInitializerSeed());
        }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactStatus> ContactStatuses { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> warehouses { get; set; }
        public DbSet<WarehouseItem> warehouseItems { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<DeliveryTerm> DeliveryTerms { get; set; }
        public DbSet<DocumentParity> DocumentParities { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<OfferItem> OfferItems { get; set; }
        public DbSet<OfferValidation> OfferValidations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public DbSet<CodePrefix> CodePrefixes { get; set; }
        public DbSet<CodeBook> CodeBooks { get; set; }
    }
}
