namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSaltPasswordReturnType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PasswordSalt", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PasswordSalt", c => c.Int(nullable: false));
        }
    }
}
