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
            CreateProductSample(context);
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

        public void CreateProductSample(OnlineShopDbContext context)
        {
            if (context.Products.Count() == 0)
            {
                var listProducts = new List<Product>()
                {
                    new Product(){Name = "Tivi Sony 4k", Alias = "tivi-sony-4k",CategoryID=2,DisplayOrder=1,Price=500,Description="Tivi Sony 4k",Content="Tivi Sony 4k",Status=true},
                    new Product(){Name = "Camera HD 4k", Alias = "camera-hd-4k",CategoryID=2,DisplayOrder=1,Price=500,Description="Camera HD 4k",Content="Camera HD 4k",Status=true},
                    new Product(){Name = "Play station 4", Alias = "ps-4",CategoryID=2,DisplayOrder=1,Price=500,Description="Play station 4",Content="Play station 4",Status=true},
                    new Product(){Name = "Dell laptop 2012", Alias = "dell-laptop-2012",CategoryID=2,DisplayOrder=1,Price=500,Description="Dell laptop 2012",Content="Dell laptop 2012",Status=true},
                    new Product(){Name = "Iphone 6", Alias = "iphone-6",CategoryID=2,DisplayOrder=1,Price=500,Description="Iphone 6",Content="Iphone 6",Status=true},
                };
                context.Products.AddRange(listProducts);
                context.SaveChanges();
            }
        }
    }
}
