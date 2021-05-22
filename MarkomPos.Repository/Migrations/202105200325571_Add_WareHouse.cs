namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_WareHouse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WarehouseItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WarehouseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CurrentQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReservedQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.WarehouseId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarehouseItems", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.WarehouseItems", "ProductId", "dbo.Products");
            DropIndex("dbo.WarehouseItems", new[] { "ProductId" });
            DropIndex("dbo.WarehouseItems", new[] { "WarehouseId" });
            DropTable("dbo.Warehouses");
            DropTable("dbo.WarehouseItems");
        }
    }
}
