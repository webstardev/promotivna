namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderReference_OrderItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "Offer_ID", "dbo.Offers");
            DropIndex("dbo.OrderItems", new[] { "Offer_ID" });
            CreateIndex("dbo.OrderItems", "OrderId");
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders", "ID", cascadeDelete: true);
            DropColumn("dbo.OrderItems", "Offer_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Offer_ID", c => c.Int());
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            CreateIndex("dbo.OrderItems", "Offer_ID");
            AddForeignKey("dbo.OrderItems", "Offer_ID", "dbo.Offers", "ID");
        }
    }
}
