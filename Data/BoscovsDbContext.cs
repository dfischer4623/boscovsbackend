using Microsoft.EntityFrameworkCore;
using BoscovsBackend.Models;

namespace BoscovsBackend.Data
{
    public class BoscovsDbContext(DbContextOptions<BoscovsDbContext> options) : DbContext(options)
    {

        // These properties represent our tables in the SQLite database
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }

        // Step 1: This is where we "Seed" the data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Seed a default Admin User
            modelBuilder.Entity<User>().HasData(new User
            {
                id = 1,
                username = "admin",
                email = "admin@boscovs.com",
                // This is a BCrypt hash for the password "password123"
                password = BCrypt.Net.BCrypt.HashPassword("password123"),
                status = "Active",
                access = "admin",
                created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            // 2. Seed some initial Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    id = 1,
                    employee_number = "1001",
                    first_name = "Mark",
                    last_name = "Fischer",
                    department = "IT",
                    job_title = "Developer",
                    status = "Active",
                    created_at = DateTime.Now
                },
                new Employee
                {
                    id = 2,
                    employee_number = "1002",
                    first_name = "John",
                    last_name = "Doe",
                    department = "Sales",
                    job_title = "Manager",
                    status = "Active",
                    created_at = DateTime.Now
                }
            );
        }
    }
}