namespace OnlineShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OnlineShop.Model.Model;
    using System;
    using System.Collections.Generic;
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
            CreateProductCategorySample(context);
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new OnlineShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new OnlineShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "onlineShop",
            //    Email = "onlineShop@gmail.com",
            //    EmailConfirmed = true,
            //    Birthday = DateTime.Now,
            //    Fullname = "Technology Education"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("onlineShop@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

        }

        public void CreateProductCategorySample(OnlineShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                var listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory(){Name = "Electronic",Alias = "electronic",Description = "Electronic",ParentID =1 , DisplayOrder =1,HomeFlag =false , Status =true},
                    new ProductCategory(){Name = "Garden",Alias = "garden",Description = "garden",ParentID =1 , DisplayOrder =1,HomeFlag =false , Status =true},
                    new ProductCategory(){Name = "Console",Alias = "console",Description = "console",ParentID =1 , DisplayOrder =1,HomeFlag =false , Status =true},
                    new ProductCategory(){Name = "Clothes",Alias = "clothes",Description = "Clothes",ParentID =1 , DisplayOrder =1,HomeFlag =false , Status =true},
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }
    }
}
