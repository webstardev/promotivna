namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_enum_ProductGroupTypeEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductGroups", "productGroupType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductGroups", "productGroupType");
        }
    }
}
