namespace SMPR_testing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskId = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        TaskTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.TaskTypes", t => t.TaskTypeId, cascadeDelete: true)
                .Index(t => t.TestId)
                .Index(t => t.TaskTypeId);
            
            CreateTable(
                "dbo.PriceDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: false)
                .Index(t => t.SessionId)
                .Index(t => t.UserId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        GroupId = c.Int(nullable: false),
                        IsCalculated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: false)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: false)
                .Index(t => t.TestId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Group_Subject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        SubjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.SubjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TestLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        Message = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        LoginName = c.String(nullable:false, maxLength:25),
                        Password = c.String(nullable: false),
                        RoleId = c.Int(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Student_Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceDataId = c.Int(nullable: false),
                        GivenAnswer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceDatas", t => t.PriceDataId, cascadeDelete: true)
                .Index(t => t.PriceDataId);
            
            CreateTable(
                "dbo.TaskTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "TaskTypeId", "dbo.TaskTypes");
            DropForeignKey("dbo.PriceDatas", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Student_Answer", "PriceDataId", "dbo.PriceDatas");
            DropForeignKey("dbo.PriceDatas", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.Sessions", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Subjects", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tests", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PriceDatas", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.TestLogs", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Tasks", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Sessions", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Group_Subject", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Group_Subject", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Answers", "TaskId", "dbo.Tasks");
            DropIndex("dbo.Student_Answer", new[] { "PriceDataId" });
            DropIndex("dbo.Users", new[] { "GroupId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.TestLogs", new[] { "TestId" });
            DropIndex("dbo.Tests", new[] { "UserId" });
            DropIndex("dbo.Tests", new[] { "SubjectId" });
            DropIndex("dbo.Subjects", new[] { "UserId" });
            DropIndex("dbo.Group_Subject", new[] { "GroupId" });
            DropIndex("dbo.Group_Subject", new[] { "SubjectId" });
            DropIndex("dbo.Sessions", new[] { "GroupId" });
            DropIndex("dbo.Sessions", new[] { "TestId" });
            DropIndex("dbo.PriceDatas", new[] { "TaskId" });
            DropIndex("dbo.PriceDatas", new[] { "UserId" });
            DropIndex("dbo.PriceDatas", new[] { "SessionId" });
            DropIndex("dbo.Tasks", new[] { "TaskTypeId" });
            DropIndex("dbo.Tasks", new[] { "TestId" });
            DropIndex("dbo.Answers", new[] { "TaskId" });
            DropTable("dbo.TaskTypes");
            DropTable("dbo.Student_Answer");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.TestLogs");
            DropTable("dbo.Tests");
            DropTable("dbo.Subjects");
            DropTable("dbo.Group_Subject");
            DropTable("dbo.Groups");
            DropTable("dbo.Sessions");
            DropTable("dbo.PriceDatas");
            DropTable("dbo.Tasks");
            DropTable("dbo.Answers");
        }
    }
}
