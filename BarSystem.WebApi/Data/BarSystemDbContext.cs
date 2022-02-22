using BarSystem.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarSystem.WebApi.Data
{
    public class BarSystemDbContext : DbContext
    {
        public BarSystemDbContext(DbContextOptions<BarSystemDbContext> options) : base(options) { }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}
