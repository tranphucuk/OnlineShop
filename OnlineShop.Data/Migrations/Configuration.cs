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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OnlineShop.Data.OnlineShopDbContext context)
        {
            CreateProductCategorySample(context);
            CreateProductSample(context);
            CreateSlide(context);
            CreatePageSample(context);
            CreateContactDetails(context);
            CreateApplicationGroup(context);
            CreateSampleAppRole(context);
            CreateLogoSample(context);
        }

        private void CreatePageSample(OnlineShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name = "about",
                    Alias = "about",
                    Content = @"What is Lorem Ipsum?
                    Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                    Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
                    when an unknown printer took a galley of type and scrambled it to make a type specimen book. 
                    It has survived not only five centuries, but also the leap into electronic typesetting,
                    remaining essentially unchanged. It was popularised in the 1960s with the release of
                    Letraset sheets containing Lorem Ipsum passages,and more recently with desktop publishing 
                    software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Status = true,

                };

                context.Pages.Add(page);
                context.SaveChanges();
            }
        }

        public void CreateUser(OnlineShopDbContext context)
        {
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

        public void CreateSlide(OnlineShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                var listSlide = new List<Slide>
                {
                    new Slide(){
                        Name = "Slide1",
                        DisplayOrder =1,
                        Status = true,
                        URL = "#",
                        Image = "/Assets/Client/images/bag.jpg",
                        Content = @"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et</p>
                                <span class=""on-get"">GET NOW</span>"},

                    new Slide(){
                        Name = "Slide2",
                        DisplayOrder =2,
                        Status = true,
                        URL = "#",
                        Image = "/Assets/Client/images/bag1.jpg",
                        Content = @"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et</p>
                                <span class=""on-get"">GET NOW</span>"},
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        public void CreateContactDetails(OnlineShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                var contactDetail = new ContactDetail()
                {
                    Name = "Nga Vy Cosmetic",
                    Address = "287 Ton Duc Thang-Le Chan-Hai Phong",
                    Email = "ngavy.cosmetic@gmail.com",
                    Phone = "0977838569",
                    Status = true,
                    Other = ""
                };
                context.ContactDetails.Add(contactDetail);
                context.SaveChanges();
            }
        }

        public void CreateApplicationGroup(OnlineShopDbContext context)
        {
            if (context.ApplicationGroups.Count() == 0)
            {
                var listAppGroupUser = new List<ApplicationGroup>()
                {
                    new ApplicationGroup(){Name="Admin",Description="Admin group"},
                    new ApplicationGroup(){Name="User",Description="User group"},
                };

                context.ApplicationGroups.AddRange(listAppGroupUser);
                context.SaveChanges();
            }
        }

        public void CreateSampleAppRole(OnlineShopDbContext context)
        {
            if (context.ApplicationRoles.Count() == 0)
            {
                var listRoles = new List<ApplicationRole>()
                {
                    new ApplicationRole(){Name = "Add user",Description = "Add new user"},
                    new ApplicationRole(){Name = "View user",Description = "View list user"},
                    new ApplicationRole(){Name = "Add product",Description = "Add new product"},
                };
                context.ApplicationRoles.AddRange(listRoles);
                context.SaveChanges();
            }
        }

        public void CreateLogoSample(OnlineShopDbContext context)
        {
            if(context.Logos.Count() == 0)
            {
                var listLogo = new List<Logo>
                {
                    new Logo()
                    {
                        Name = "NgaVy Cosmetic",
                        CreatedDate = DateTime.Now,
                        ImagePath = "imageslogo.png",
                        Status = true
                    }
                };
                context.Logos.AddRange(listLogo);
                context.SaveChanges();
            }
        }
    }
}
