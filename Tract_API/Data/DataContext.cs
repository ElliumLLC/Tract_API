using Tract_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Tract_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Tract> tracts { get; set; }
    }
}
