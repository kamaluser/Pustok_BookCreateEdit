using Microsoft.EntityFrameworkCore;
using Pustok_MVC.Models;

namespace Pustok_MVC.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Setting> Settings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookTag>().HasKey(x => new { x.BookId, x.TagId });
        }
    }
}
