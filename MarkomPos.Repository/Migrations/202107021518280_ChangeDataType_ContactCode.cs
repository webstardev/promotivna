namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDataType_ContactCode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Code", c => c.Int(nullable: false));
        }
    }
}
