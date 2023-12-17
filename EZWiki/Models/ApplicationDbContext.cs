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

        internal static void SeedData(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Articles.Any(a => a.Topic == "Home")) return;
            
            context.Articles.Add(new Article
            {
                Topic = "Home",
                Content = "Welcome to EZWiki! This is the default home page, please change",
                Published = NodaTime.SystemClock.Instance.GetCurrentInstant()
            });
            context.SaveChanges();
        }
    }
}
