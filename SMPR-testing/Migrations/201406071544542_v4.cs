namespace SMPR_testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            Sql("insert into [TaskTypes] values ('Много из многих')");
            Sql("insert into [TaskTypes] values ('Один из двух')");
        }
        
        public override void Down()
        {
        }
    }
}
