namespace MVCOtel_2.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCOtel_2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MVCOtel_2.Models.ApplicationDbContext ctx)
        {

            if (ctx.Roles.Where(x => x.Name == "Admin").Count() == 0) //Adý Admin olana Roles yoksa eklesin. null deðil, Count olsun.
            {
                var rstore = new RoleStore<IdentityRole>(ctx);
                var rmanager = new RoleManager<IdentityRole>(rstore);
                var role = new IdentityRole { Name = "Admin" };
                rmanager.Create(role);
            }

            if (ctx.Users.Where(x => x.Email == "m@m.com").Count() == 0) //Kullanýcý yoksa ekle. Email ile UserName ayný
            {
                var kmanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
                var user = new ApplicationUser { UserName = "m@m.com", Email = "m@m.com" };
                kmanager.Create(user, "123456");
                kmanager.AddToRole(user.Id, "Admin");
            }
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
        }
    }
}
