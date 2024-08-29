
using EntiyFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EntiyFrameworkCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}