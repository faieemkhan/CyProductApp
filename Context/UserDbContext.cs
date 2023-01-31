using Microsoft.EntityFrameworkCore;

using MVCCoreApplication.Models;

namespace MVCCoreApplication.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        {
          /*Database.EnsureCreated();*/

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<pluralizingTableNameConvention>();
        //}

        //Database table -users
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<OrderTable> Orders { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }

        
        public DbSet<MVCCoreApplication.Models.UserLoginCredential> UserLoginCredential { get; set; }
        public DbSet<MVCCoreApplication.Models.AdminLoginCredentials> AdminLoginCredentials { get; set; }
       
    }
}
