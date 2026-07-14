using CerGlazerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CerGlazerAPI.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
    }
}
