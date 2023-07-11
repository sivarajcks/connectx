using ConnectX.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectX.Data_Access_Layer
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-3HMAF17;Database=conx;Trusted_Connection=True;");
            }
        }
    }
}
