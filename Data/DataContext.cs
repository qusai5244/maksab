using Maksab.Models;
using Microsoft.EntityFrameworkCore;

namespace Maksab.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToRole> UsersToRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleToPermission> RoleToPermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

    }
}
