using EmployeeTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Mission> Missions { get; set; }
    }
}
