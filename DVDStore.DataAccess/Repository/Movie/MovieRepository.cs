using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
        public List<Person> ExtractPeople(MovieViewModel mvm, string relationship)
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
                Id = mcm.Id,
                Title = mcm.Title,
                ReleaseDate = mcm.ReleaseDate,
                Price = mcm.Price,
                Price5 = mcm.Price5,
                Price10 = mcm.Price10,
                Summary = mcm.Summary,
                CategoryId = mcm.CategoryId,
                DirectorId = mcm.DirectorId,
                Actors = Actors,
                Writers = Writers
            };   
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
                mvm.Actors.Add(new PersonCheckModel() {Person = person, isChecked = _db.People.Any(p => p.Id == person.Id ? true : false)});
                mvm.Writers.Add(new PersonCheckModel() {Person = person, isChecked = _db.People.Any(p => p.Id == person.Id ? true : false)});
                i++;
            }
            return mvm;
        }
        public void Update(Movie movie)
        {
            bool movieFound = _db.Movies.Any(m => m.Id == movie.Id);
            if (movieFound)
            {
                _db.Movies.Update(movie);
            }
        }
    }
}
