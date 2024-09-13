using Microsoft.EntityFrameworkCore;
using ZimoziAssesment.Data.Entities;

namespace ZimoziAssesment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
