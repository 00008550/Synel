using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataFolder
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
