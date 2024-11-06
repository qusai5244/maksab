using Maksab.Models;
using Microsoft.EntityFrameworkCore;

namespace Maksab.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Shop> Shops { get; set; }
        //public DbSet<User> Users { get; set; }
    }
}
