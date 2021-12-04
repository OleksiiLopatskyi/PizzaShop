using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Models
{
    public class PizzaContext:DbContext
    {
        public DbSet<UserAccounts> UserAccounts { get; set; }
        public DbSet<UserAccounts> User { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShopCart> ShopCart { get; set; }
        public DbSet<Order> Orders { get; set; }
        public PizzaContext(DbContextOptions<PizzaContext>options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRole = "admin";
            string userRole = "user";
            string adminEmail = "admin@gmail.com";
            string adminName = "Admin";
            string adminPhone = "380688303152";
            string adminPassword = "12345";

            Role admin = new Role { Name = adminRole, Id = 1 };
            Role user = new Role { Name = userRole, Id = 2 };

            UserAccounts adminAccount = new UserAccounts {Id=1,Email=adminEmail,Phone=adminPhone,Name=adminName,Password=adminPassword,RoleId=1,Type=UserType.golden};
            
            modelBuilder.Entity<Role>().HasData(new Role[] {admin,user});
            modelBuilder.Entity<UserAccounts>().HasData(new UserAccounts[] {adminAccount});
            base.OnModelCreating(modelBuilder);
        }
    }
}
