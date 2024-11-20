using Microsoft.EntityFrameworkCore;
using DVDStore_RazorPages.Models;

namespace DVDStore_RazorPages.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}
        public DbSet<Category> Categories {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, DisplayOrder = 1, Name = "Horror"},
                new Category { Id = 2, DisplayOrder = 2, Name = "Action" },
                new Category { Id = 3, DisplayOrder = 3, Name = "Thriller" });
        }
    }
}
