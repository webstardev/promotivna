namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                        DateTimeValidFrom = c.DateTime(nullable: false),
                        DateTimeValidTo = c.DateTime(),
                        ContactStatusId = c.Int(nullable: false),
                        Note = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.ContactStatus", t => t.ContactStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ContactId)
                .Index(t => t.ContactStatusId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        OIB = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        CountryCode = c.String(),
                        Phone = c.String(),
                        Phone2 = c.String(),
                        Fax = c.String(),
                        MobilePhone = c.String(),
                        Email = c.String(),
                        WebAddress = c.String(),
                        Person = c.String(),
                        AccountNumber = c.String(),
                        CreditLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HasWarranty = c.Boolean(nullable: false),
                        WarrantyNote = c.String(),
                        Note = c.String(),
                        Note2 = c.String(),
                        IsBuyer = c.Boolean(nullable: false),
                        IsSupplier = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContactStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        Note = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(),
                        Address = c.String(nullable: false),
                        JobDescription = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Email = c.String(),
                        MobilePhone = c.String(),
                        Note = c.String(),
                        Note2 = c.String(),
                        ColorHex = c.String(),
                        Username = c.String(),
                        PasswordSalt = c.Int(nullable: false),
                        PasswordHash = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayName = c.String(),
                        ParrentGroupId = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductGroups", t => t.ParrentGroupId)
                .Index(t => t.ParrentGroupId);
            
            CreateTable(
                "dbo.ProductInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                        ExternalCode = c.String(),
                        InputPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarkUp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OutputPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Default = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        Note = c.String(),
                        Note2 = c.String(),
                        Code = c.Int(nullable: false),
                        UnitOfMeasureId = c.Int(),
                        ProductGroupId = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId)
                .ForeignKey("dbo.UnitOfMeasures", t => t.UnitOfMeasureId)
                .Index(t => t.UnitOfMeasureId)
                .Index(t => t.ProductGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductInfoes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "UnitOfMeasureId", "dbo.UnitOfMeasures");
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductInfoes", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.ProductGroups", "ParrentGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ContactInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.ContactInfoes", "ContactStatusId", "dbo.ContactStatus");
            DropForeignKey("dbo.ContactInfoes", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            DropIndex("dbo.Products", new[] { "UnitOfMeasureId" });
            DropIndex("dbo.ProductInfoes", new[] { "ContactId" });
            DropIndex("dbo.ProductInfoes", new[] { "ProductId" });
            DropIndex("dbo.ProductGroups", new[] { "ParrentGroupId" });
            DropIndex("dbo.ContactInfoes", new[] { "ContactStatusId" });
            DropIndex("dbo.ContactInfoes", new[] { "ContactId" });
            DropIndex("dbo.ContactInfoes", new[] { "UserId" });
            DropTable("dbo.Products");
            DropTable("dbo.ProductInfoes");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.Users");
            DropTable("dbo.ContactStatus");
            DropTable("dbo.Contacts");
            DropTable("dbo.ContactInfoes");
        }
    }
}
