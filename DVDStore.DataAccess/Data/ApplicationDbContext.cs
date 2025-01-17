using MovieStore.Models;
using Microsoft.EntityFrameworkCore;
namespace MovieStore.DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Horror", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            );

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Director)
                .WithMany(p => p.MoviesDirected);
             
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Writers)
                .WithMany(p => p.MoviesWrittenFor);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(p => p.MoviesStarredIn)
                .UsingEntity<MoviesActors>(
                    r => r.HasOne<Person>().WithMany().HasForeignKey(r => r.ActorId),
                    l => l.HasOne<Movie>().WithMany().HasForeignKey(l => l.MovieId)
                    .OnDelete(DeleteBehavior.Restrict));

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Writers)
                .WithMany(w => w.MoviesWrittenFor)
                .UsingEntity<MoviesWriters>(
                    r => r.HasOne<Person>().WithMany().HasForeignKey(r => r.WriterId),
                    l => l.HasOne<Movie>().WithMany().HasForeignKey(l => l.MovieId)
                    .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
