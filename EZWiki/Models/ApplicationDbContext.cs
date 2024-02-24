using Microsoft.EntityFrameworkCore;

namespace EZWiki.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasIndex(a => a.Slug).IsUnique();
        }

        internal static void SeedData(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Articles.Any(a => a.Topic == "Home")) return;
            
            context.Articles.Add(new Article
            {
                Id = 1,
                Topic = "Home",
                Slug = "home",
                Content = "Welcome to EZWiki! This is the default home page, please change",
                Published = NodaTime.SystemClock.Instance.GetCurrentInstant()
            });
            context.SaveChanges();
        }
    }
}
