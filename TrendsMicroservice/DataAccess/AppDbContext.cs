using DataAccess.Configurations;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Trend> Trends { get; set; }
        
        public DbSet<Post> Posts { get; set; }
        
        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<TrendFollow> TrendFollows { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // https://docs.microsoft.com/en-us/ef/core/modeling/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TrendConfiguration());

            modelBuilder.ApplyConfiguration(new PostConfiguration());

            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new CommentReactConfiguration());

            modelBuilder.ApplyConfiguration(new PostReactConfiguration());

            modelBuilder.ApplyConfiguration(new TrendFollowConfiguration());
        }
    }
}
