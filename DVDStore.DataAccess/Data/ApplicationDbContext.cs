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
        public DbSet<MoviesActors> MoviesActors {get; set;}
        public DbSet<MoviesWriters> MoviesWriters {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Horror", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Drama", DisplayOrder = 4 }
            );

            modelBuilder.Entity<Movie>().HasData(
                    new Movie { Id = 1, 
                        Title = "The Father", 
                        ReleaseDate = new DateOnly(2020, 1, 27), 
                        CategoryId = 4, 
                        Summary = "The Father is a 2020 psychological drama film, directed by Florian Zeller in his directorial debut. " +
                        "The film stars Anthony Hopkins as an octogenarian Welsh man living with dementia. " +
                        "At the 93rd Academy Awards, The Father received six nominations, including Best Picture; Hopkins won Best Actor and Zeller and Hampton won Best Adapted Screenplay. " +
                        "Since then, it has been cited as one of the best films of the 2020s and the 21st century.",
                        DirectorId = 2,
                        ImageUrl = ""
                    }
                );

            modelBuilder.Entity<MoviesActors>().HasData(
                    new MoviesActors
                    {
                        ActorId = 1,
                        MovieId = 1
                    });
            modelBuilder.Entity<MoviesWriters>().HasData(
                    new MoviesWriters
                    {
                        MovieId = 1,
                        WriterId = 2,
                    });

            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    Name = "Anthony Hopkins",
                    Background = " One of Britain's most recognisable and prolific actors, he is known for his performances on the screen and stage. Hopkins has received numerous accolades, including two Academy Awards, four BAFTA Awards, two Primetime Emmy Awards, and a Laurence Olivier Award."
                },   
                new Person
                {
                    Id = 2,
                    Name = "Florian Zeller",
                    Background = "Florian Zeller is a French novelist, playwright, theatre director, screenwriter, and film director. He has written over a dozen plays, that have been staged worldwide and have made him one of the most celebrated contemporary playwrights."
                }
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
                    l => l.HasOne<Movie>().WithMany(m => m.MoviesActors)
                    .OnDelete(DeleteBehavior.Restrict));

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Writers)
                .WithMany(w => w.MoviesWrittenFor)
                .UsingEntity<MoviesWriters>(
                    r => r.HasOne<Person>().WithMany().HasForeignKey(r => r.WriterId),
                    l => l.HasOne<Movie>().WithMany(m => m.MoviesWriters)
                    .OnDelete(DeleteBehavior.Restrict));

            modelBuilder.Entity<MoviesActors>()
                .HasKey(ma => new { ma.ActorId, ma.MovieId });
            modelBuilder.Entity<MoviesWriters>()
                .HasKey(mw => new { mw.WriterId, mw.MovieId });
        }
    }
}
