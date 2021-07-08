namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDataType_OfferNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Offers", "OfferNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Offers", "OfferNumber", c => c.Int(nullable: false));
        }
    }
}
