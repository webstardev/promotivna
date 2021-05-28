namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRole_RoleMapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoleMappings", "Roles_ID", "dbo.Roles");
            DropForeignKey("dbo.UserRoleMappings", "UserId", "dbo.Users");
            DropIndex("dbo.UserRoleMappings", new[] { "UserId" });
            DropIndex("dbo.UserRoleMappings", new[] { "Roles_ID" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoleMappings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRoleMappings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                        Roles_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Roles",
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
            
            CreateIndex("dbo.UserRoleMappings", "Roles_ID");
            CreateIndex("dbo.UserRoleMappings", "UserId");
            AddForeignKey("dbo.UserRoleMappings", "UserId", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.UserRoleMappings", "Roles_ID", "dbo.Roles", "ID");
        }
    }
}
