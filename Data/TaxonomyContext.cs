using Microsoft.EntityFrameworkCore;

namespace Taxonomix.Data
{
    public class TaxonomyContext : DbContext
    {
        public DbSet<Dataset> Datasets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=/tmp/taxonomy.db");
    }
}