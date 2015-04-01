namespace SMPR_testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5_prices_for_task_and_answer_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Tests", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "Price");
            DropColumn("dbo.Tasks", "Price");
        }
    }
}
