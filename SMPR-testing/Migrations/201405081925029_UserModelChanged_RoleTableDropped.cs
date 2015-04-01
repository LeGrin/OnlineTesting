namespace SMPR_testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelChanged_RoleTableDropped : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            AlterColumn("dbo.Users", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "RoleId");
            DropColumn("dbo.Users", "RegDate");
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "RegDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
