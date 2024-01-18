using CampusFeedApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusFeedApi.Data
{
    public class CampusFeedInfoDataContext : DbContext
    {
        protected readonly IConfiguration _config;

        public DbSet<CampusFeedInfo> CampusFeedInfo { get; set; }

        public CampusFeedInfoDataContext(
            DbContextOptions<CampusFeedInfoDataContext> options,
            IConfiguration configuration) : base(options)
        {
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Check if SQLite specific options are required
            optionsBuilder.UseSqlite(_config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CampusFeedInfo>(p =>
            {
                p.ToTable("CampusFeedInfo");
                p.HasKey(x => x.CampusFeedId);
                p.Property(x => x.CampusFeedId).IsRequired();
                p.Property(x => x.Category).IsRequired();
                p.Property(x => x.Content).IsRequired();
                p.Property(x => x.Date).IsRequired();
                p.Property(x => x.Like).IsRequired();
                p.Property(x => x.Dislike).IsRequired();
                p.HasQueryFilter(x => x.Date != DateTime.MinValue);
                p.HasQueryFilter(e => e.Like != 0 && e.Dislike != 0);
            });
        }


    }

}
