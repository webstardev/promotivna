namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRole_RoleMapping : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.UserRoleMappings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RolesId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RolesId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RolesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoleMappings", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoleMappings", "RolesId", "dbo.Roles");
            DropIndex("dbo.UserRoleMappings", new[] { "RolesId" });
            DropIndex("dbo.UserRoleMappings", new[] { "UserId" });
            DropTable("dbo.UserRoleMappings");
            DropTable("dbo.Roles");
        }
    }
}
