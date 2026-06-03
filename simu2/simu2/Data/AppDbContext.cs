using Microsoft.EntityFrameworkCore;
using simu2.Models;

namespace simu2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Chef> Chefs { get; set; }
    }
}
