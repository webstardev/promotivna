namespace MarkomPos.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodeBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodePrefixId = c.Int(nullable: false),
                        NextNumber = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CodePrefixes", t => t.CodePrefixId, cascadeDelete: true)
                .Index(t => t.CodePrefixId);
            
            CreateTable(
                "dbo.CodePrefixes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        DocumentTypeEnum = c.Int(nullable: false),
                        StartNumber = c.Int(nullable: false),
                        NewStartNumberEachYear = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CodeBooks", "CodePrefixId", "dbo.CodePrefixes");
            DropIndex("dbo.CodeBooks", new[] { "CodePrefixId" });
            DropTable("dbo.CodePrefixes");
            DropTable("dbo.CodeBooks");
        }
    }
}
