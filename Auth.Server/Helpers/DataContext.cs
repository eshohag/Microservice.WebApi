using Auth.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Helpers
{
    public class DataContext : DbContext
    {
        //For In Memory Database
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // in memory database used for simplicity, change to a real db for production applications
        //    options.UseSqlServer("TestDb");
        //}

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>().ToTable("Department");
        }
    }
}
