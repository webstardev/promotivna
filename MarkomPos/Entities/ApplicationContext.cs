using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Entities
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
        : base(options)
        {

        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            var added = this.ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Added)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in added)
            {
                if (entity is IEntity)
                {
                    var track = entity as IEntity;
                    track.DateCreated = DateTime.Now;
                }
            }

            var modified = this.ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Modified)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in modified)
            {
                if (entity is IEntity)
                {
                    var track = entity as IEntity;
                    track.DateModified = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        // ADMINISTRACIJA
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<DocumentParity> DocumentParities { get; set; }
        public DbSet<DeliveryTerm> DeliveryTerms { get; set; }
        public DbSet<CodePrefix> CodePrefixes { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserActionLog> UserActionLogs { get; set; }
        public DbSet<CodeBook> CodeBooks { get; set; }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInfo> ProductInfoes { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseItem> WarehouseItems { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactStatus> ContactStatuses { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
    }
}
