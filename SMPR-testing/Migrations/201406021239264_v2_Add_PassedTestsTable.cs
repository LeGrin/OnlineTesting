namespace SMPR_testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2_Add_PassedTestsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PassedTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        HasPassed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TestId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PassedTests", "UserId", "dbo.Users");
            DropForeignKey("dbo.PassedTests", "TestId", "dbo.Tests");
            DropIndex("dbo.PassedTests", new[] { "UserId" });
            DropIndex("dbo.PassedTests", new[] { "TestId" });
            DropTable("dbo.PassedTests");
        }
    }
}
