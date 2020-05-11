using Microsoft.EntityFrameworkCore;

namespace HttpPatchSample.Database
{
    public class HttpPatchSampleDbContext : DbContext
    {
        public HttpPatchSampleDbContext(DbContextOptions<HttpPatchSampleDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne(x => x.Mother).WithMany();
            modelBuilder.Entity<User>().HasOne(x => x.Father).WithMany();
        }
    }
}