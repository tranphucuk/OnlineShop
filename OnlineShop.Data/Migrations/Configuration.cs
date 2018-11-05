namespace OnlineShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OnlineShop.Model.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineShop.Data.OnlineShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OnlineShop.Data.OnlineShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new OnlineShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new OnlineShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "onlineShop",
                Email = "onlineShop@gmail.com",
                EmailConfirmed = true,
                Birthday = DateTime.Now,
                Fullname = "Technology Education"

            };

            manager.Create(user, "123654$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("onlineShop@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

        }
    }
}
