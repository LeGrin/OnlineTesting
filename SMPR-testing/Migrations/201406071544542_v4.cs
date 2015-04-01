namespace SMPR_testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            Sql("insert into [TaskTypes] values ('ќдин из двух')");
            Sql("insert into [TaskTypes] values ('ќдин из многих')");
        }
        
        public override void Down()
        {
        }
    }
}
