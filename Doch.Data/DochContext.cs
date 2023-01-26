using Doch.Models;
using Microsoft.EntityFrameworkCore;

namespace Doch.Data
{
    public class DochContext : DbContext
    {
        public DochContext(DbContextOptions<DochContext> options) : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position);
        }
    }
}