using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineShop.Model.Models;

namespace OnlineShop.Data.Migrations
{
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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new OnlineShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new OnlineShopDbContext()));

            //var user = new ApplicationUser
            //{
            //    UserName = "admin",
            //    Email = "nguyenhongcuong@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Nguyễn Văn A"
            //};

            //manager.Create(user, "123456");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}


            //var adminUser = manager.FindByEmail("nguyenhongcuong@gmail.com");
            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

            CreateProductCategoryExample(context);




        }


        private void CreateProductCategoryExample(OnlineShopDbContext dbContext)
        {
            if (!dbContext.ProductCategories.Any())
            {

                List<ProductCategory> productCategories = new List<ProductCategory>
            {
                new ProductCategory
                {
                    Name = "Điện lạnh",
                    Alias = "dien-lanh",
                    Status = true
                },
                 new ProductCategory
                {
                    Name = "Viễn thông",
                    Alias = "vien-thong",
                    Status = true
                },
                  new ProductCategory
                {
                    Name = "Đồ gia dụng",
                    Alias = "do-gia-dung",
                    Status = true
                },
                   new ProductCategory
                {
                    Name = "Mỹ phẩm",
                    Alias = "my-pham",
                    Status = true
                },
            };

                dbContext.ProductCategories.AddRange(productCategories);
                dbContext.SaveChanges();

            }
        }
    }
}
