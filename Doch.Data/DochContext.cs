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

            modelBuilder.Entity<Position>()
                .HasData(new Position()
                {
                    PositionId = 1,
                    PositionName = "Director"
                });
            modelBuilder.Entity<Position>()
                .HasData(new Position()
                {
                    PositionId = 2,
                    PositionName = "Manager"
                });
            modelBuilder.Entity<Position>()
                .HasData(new Position()
                {
                    PositionId = 3,
                    PositionName = "Financial"
                });
            modelBuilder.Entity<Position>()
                .HasData(new Position()
                {
                    PositionId = 4,
                    PositionName = "Buildings"
                });
            modelBuilder.Entity<Position>()
                .HasData(new Position()
                {
                    PositionId = 5,
                    PositionName = "People"
                });

            modelBuilder.Entity<Employee>()
                .HasData(new Employee()
                {
                    EmployeeId = 1,
                    Name = "Admin",
                    SurName = "Administrator",
                    BirthDate = new DateTime(2000, 1, 1),
                    PositionId = 1,
                    IpAddress = "1.1.1.1",
                    IpCountryCode = "SK"
                });
        }
    }
}