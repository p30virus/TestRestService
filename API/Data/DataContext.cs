using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppGroup> Groups { get; set; }
        public DbSet<AppGroupMembership> GroupsMembership { get; set; }
    }
}