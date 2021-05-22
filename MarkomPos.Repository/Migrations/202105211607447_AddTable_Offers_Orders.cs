namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_Offers_Orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryTerms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DocumentParities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OfferItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OfferId = c.Int(nullable: false),
                        Ordinal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Int(),
                        ProductName = c.String(nullable: false),
                        UnitOfMeasureId = c.Int(nullable: false),
                        UnitOfMeasureName = c.String(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Porez = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Offers", t => t.OfferId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.UnitOfMeasures", t => t.UnitOfMeasureId, cascadeDelete: true)
                .Index(t => t.OfferId)
                .Index(t => t.ProductId)
                .Index(t => t.UnitOfMeasureId);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OfferDate = c.DateTime(nullable: false),
                        OfferNumber = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        DeliveryTermId = c.Int(nullable: false),
                        DocumentParityId = c.Int(nullable: false),
                        PaymentMethodId = c.Int(nullable: false),
                        ResponsibleUserId = c.Int(nullable: false),
                        ContactId = c.Int(),
                        ContactName = c.String(nullable: false),
                        ContactAddress = c.String(),
                        ContactCountry = c.String(),
                        PrintNote = c.Boolean(nullable: false),
                        Note = c.String(),
                        Note2 = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .ForeignKey("dbo.DeliveryTerms", t => t.DeliveryTermId, cascadeDelete: true)
                .ForeignKey("dbo.DocumentParities", t => t.DocumentParityId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ResponsibleUserId, cascadeDelete: true)
                .Index(t => t.DeliveryTermId)
                .Index(t => t.DocumentParityId)
                .Index(t => t.PaymentMethodId)
                .Index(t => t.ResponsibleUserId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OfferValidations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserDiplayName = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        OfferId = c.Int(nullable: false),
                        Note = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Offers", t => t.OfferId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.OfferId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        Ordinal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false),
                        ShortDescription = c.String(),
                        UnitOfMeasureId = c.Int(nullable: false),
                        UnitOfMeasureName = c.String(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Porez = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                        Offer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Offers", t => t.Offer_ID)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.UnitOfMeasures", t => t.UnitOfMeasureId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UnitOfMeasureId)
                .Index(t => t.Offer_ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsExternalOrder = c.Boolean(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        DeliveryTermId = c.Int(nullable: false),
                        DocumentParityId = c.Int(nullable: false),
                        PaymentMethodId = c.Int(nullable: false),
                        ResponsibleUserId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                        ContactName = c.String(nullable: false),
                        ContactAddress = c.String(),
                        ContactCountry = c.String(),
                        PrintNote = c.Boolean(nullable: false),
                        Note = c.String(),
                        Note2 = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.DeliveryTerms", t => t.DeliveryTermId, cascadeDelete: true)
                .ForeignKey("dbo.DocumentParities", t => t.DocumentParityId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ResponsibleUserId, cascadeDelete: true)
                .Index(t => t.DeliveryTermId)
                .Index(t => t.DocumentParityId)
                .Index(t => t.PaymentMethodId)
                .Index(t => t.ResponsibleUserId)
                .Index(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ResponsibleUserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods");
            DropForeignKey("dbo.Orders", "DocumentParityId", "dbo.DocumentParities");
            DropForeignKey("dbo.Orders", "DeliveryTermId", "dbo.DeliveryTerms");
            DropForeignKey("dbo.Orders", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.OrderItems", "UnitOfMeasureId", "dbo.UnitOfMeasures");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "Offer_ID", "dbo.Offers");
            DropForeignKey("dbo.OfferValidations", "UserId", "dbo.Users");
            DropForeignKey("dbo.OfferValidations", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.OfferItems", "UnitOfMeasureId", "dbo.UnitOfMeasures");
            DropForeignKey("dbo.OfferItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OfferItems", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.Offers", "ResponsibleUserId", "dbo.Users");
            DropForeignKey("dbo.Offers", "PaymentMethodId", "dbo.PaymentMethods");
            DropForeignKey("dbo.Offers", "DocumentParityId", "dbo.DocumentParities");
            DropForeignKey("dbo.Offers", "DeliveryTermId", "dbo.DeliveryTerms");
            DropForeignKey("dbo.Offers", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Orders", new[] { "ContactId" });
            DropIndex("dbo.Orders", new[] { "ResponsibleUserId" });
            DropIndex("dbo.Orders", new[] { "PaymentMethodId" });
            DropIndex("dbo.Orders", new[] { "DocumentParityId" });
            DropIndex("dbo.Orders", new[] { "DeliveryTermId" });
            DropIndex("dbo.OrderItems", new[] { "Offer_ID" });
            DropIndex("dbo.OrderItems", new[] { "UnitOfMeasureId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OfferValidations", new[] { "OfferId" });
            DropIndex("dbo.OfferValidations", new[] { "UserId" });
            DropIndex("dbo.Offers", new[] { "ContactId" });
            DropIndex("dbo.Offers", new[] { "ResponsibleUserId" });
            DropIndex("dbo.Offers", new[] { "PaymentMethodId" });
            DropIndex("dbo.Offers", new[] { "DocumentParityId" });
            DropIndex("dbo.Offers", new[] { "DeliveryTermId" });
            DropIndex("dbo.OfferItems", new[] { "UnitOfMeasureId" });
            DropIndex("dbo.OfferItems", new[] { "ProductId" });
            DropIndex("dbo.OfferItems", new[] { "OfferId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.OfferValidations");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.Offers");
            DropTable("dbo.OfferItems");
            DropTable("dbo.DocumentParities");
            DropTable("dbo.DeliveryTerms");
        }
    }
}
