namespace SMPR_testing.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<SMPR_testing_Lib.Repository.SmprDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SMPR_testing_Lib.Repository.SmprDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "LoginName", autoCreateTables: true);

            //adding default group for all users before their details are written to the Users table
            //if (!(context.Groups.Where(x => x.Name == "Default").Count() > 0))
            //{
            //    context.Groups.RemoveRange(context.Groups);
            //    context.Groups.Add(new SMPR_testing_Lib.Domain.Group() { Name = "Default" });
            //}

            //adding admin group
            if (!(context.Groups.Where(x => x.Name == "Администраторы").Count() > 0))
            {
                context.Groups.Add(new SMPR_testing_Lib.Domain.Group() { Name = "Администраторы" });
                context.SaveChanges();
            }

            if (!(context.Groups.Where(x => x.Name == "Преподаватели").Count() > 0))
            {
                context.Groups.Add(new SMPR_testing_Lib.Domain.Group() { Name = "Преподаватели" });
                context.SaveChanges();
            }

            #region adding admin role and account, mathing them in UserToRole table
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("admin"))
            {
                roles.CreateRole("admin");
            }

            if (membership.GetUser("admin", false) == null)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("Name", "superadmin");
                values.Add("GroupId", context.Groups.Where(x => x.Name == "Администраторы").First().Id);

                membership.CreateUserAndAccount("admin", "smpr2014",values);
                #region Setting details of admin user in Users table
                //var k = context.Users.Where(x=>x.LoginName == "admin");
                //if(k.Count() > 0)
                //{
                    
                //    SMPR_testing_Lib.Domain.User adminUser = k.First();
                //    adminUser.Name = "superadmin";
                //    adminUser.GroupId = context.Groups.Where(x => x.Name == "Admins").First().Id;
                //    context.SaveChanges();
                //}
                #endregion
            }
            if (!roles.GetRolesForUser("admin").Contains("admin"))
            {
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "admin" });
            }
            #endregion

            

            //adding roles
            if (!roles.RoleExists("lecturer"))
            {
                roles.CreateRole("lecturer");
            }
            if (!roles.RoleExists("student"))
            {
                roles.CreateRole("student");
            }
        }
    }
}
