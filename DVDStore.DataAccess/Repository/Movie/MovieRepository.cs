using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess.Data;
using MovieStore.Models;


namespace MovieStore.DataAccess.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext db) : base(db) { }
        public Movie? Get(Expression<Func<Movie, bool>> exp)
        {
            Movie? movie = _db.Movies.FirstOrDefault(exp);
            _db.Entry(movie).Reference(m => m.Director).Load();
            _db.Entry(movie).Collection(m => m.Writers).Load();
            _db.Entry(movie).Collection(m => m.Actors).Load();
            return movie;
        }

        public List<Movie> Filter(Expression<Func<Movie, bool>> filter)
        {
            List<Movie> movies =_db.Movies.Where(filter).ToList();
            foreach(var movie in movies)
            {
                _db.Entry(movie).Reference(m => m.Director).Load();
                _db.Entry(movie).Collection(m => m.Writers).Load();
                _db.Entry(movie).Collection(m => m.Actors).Load();
            }
            return movies;
        }
        public List<Person> ExtractCheckedPeople(MovieViewModel mvm, string relationship)
        {
            List<Person> people = new List<Person>();
            switch(relationship)
            {
                case "Actors":
                    people = mvm.Actors.Where(a => a.isChecked).Select(a => a.Person).ToList();
                    break;
                case "Writers":
                    people = mvm.Writers.Where(a => a.isChecked).Select(a => a.Person).ToList();
                    break;
            }
            return people;
        }

        public Movie InstantiateMovie(MovieViewModel mcm, List<Person> Actors, List<Person> Writers)
        {
            Movie movie = new Movie()
            {
                Id = mcm.Id == null ?_db.Movies.AsNoTracking().Count() + 1 : mcm.Id,
                Title = mcm.Title,
                ReleaseDate = mcm.ReleaseDate,
                Price = mcm.Price,
                Price5 = mcm.Price5,
                Price10 = mcm.Price10,
                Summary = mcm.Summary,
                CategoryId = mcm.CategoryId,
                DirectorId = mcm.DirectorId,
            };
            foreach(var actor in Actors)
            {
                movie.MoviesActors.Add(new MoviesActors() {ActorId=actor.Id, MovieId=movie.Id});
            }
            foreach(var writer in Writers)
            {
                movie.MoviesWriters.Add(new MoviesWriters() {WriterId=writer.Id, MovieId=movie.Id});
            }
            return movie;
        }
        public MovieViewModel InstantiateMovieViewModel(Movie movie)
        {
            MovieViewModel mvm = new MovieViewModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Price = movie.Price,
                Price5 = movie.Price5,
                Price10 = movie.Price10,
                Summary = movie.Summary,
                CategoryId = movie.CategoryId,
                DirectorId = movie.DirectorId,
            };

            var i=0;
            foreach (var person in _db.People.ToList())
            {
                mvm.Actors.Add(new PersonCheckModel() {Person = person, isChecked = movie.Actors.Any(a => a.Id == person.Id ? true : false)});
                mvm.Writers.Add(new PersonCheckModel() {Person = person, isChecked = movie.Writers.Any(a => a.Id == person.Id ? true : false)});
                i++;
            }
            return mvm;
        }
        public void Update(Movie movie)
        {
            Movie? _movie = _db.Movies
                            .Include(m => m.MoviesActors)
                            .Include(m => m.MoviesWriters)
                            .FirstOrDefault(m => m.Id == movie.Id);
            _db.Entry(_movie).CurrentValues.SetValues(movie);
            var newMoviesActors = movie.MoviesActors.Where(ma => !_movie.MoviesActors.Select(ma2 => ma2.ActorId).Any(actorId => actorId == ma.ActorId));
            var obsoleteMoviesActors = _movie.MoviesActors.Where(ma => !movie.MoviesActors.Select(ma2 => ma2.ActorId).Any(actorId => actorId == ma.ActorId)).ToList();
            int obsoleteMoviesActorsCount = obsoleteMoviesActors.Count();
            for (int i = 0; i < obsoleteMoviesActorsCount; i++)
            {
                _movie.MoviesActors.Remove(obsoleteMoviesActors[i]);
            }
            _movie.MoviesActors.AddRange(newMoviesActors);

            var newMoviesWriters = movie.MoviesWriters.Where(ma => !_movie.MoviesWriters.Select(ma2 => ma2.WriterId).Any(writerId => writerId == ma.WriterId));
            var obsoleteMoviesWriters = _movie.MoviesWriters.Where(ma => !movie.MoviesWriters.Select(ma2 => ma2.WriterId).Any(writerId => writerId == ma.WriterId)).ToList();
            int obsoleteMoviesWritersCount = obsoleteMoviesWriters.Count();
            for (int i=0; i < obsoleteMoviesWritersCount; i++)
            {
                _movie.MoviesWriters.Remove(obsoleteMoviesWriters[i]);
            }
            _movie.MoviesWriters.AddRange(newMoviesWriters);
        }
        public override void Delete(Movie movie)
        {
            foreach(var ma in movie.MoviesActors)
            {
                _db.MoviesActors.Remove(ma);
            }
            foreach(var mw in movie.MoviesWriters)
            {
                _db.MoviesWriters.Remove(mw);
            }
            movie.MoviesWriters.Clear();
            _db.Movies.Remove(movie);
        }
    }
}
