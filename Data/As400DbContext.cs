using Microsoft.EntityFrameworkCore;
using BoscovsBackend.Models;

namespace BoscovsBackend.Data
{
    public class As400DbContext : DbContext
    {
        public As400DbContext(DbContextOptions<As400DbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        // You will eventually add other models here, like Employees and Salaries
    }
}