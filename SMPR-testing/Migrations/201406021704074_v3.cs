namespace SMPR_testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "TestPassed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "TestPassed", c => c.Boolean(nullable: false));
        }
    }
}
