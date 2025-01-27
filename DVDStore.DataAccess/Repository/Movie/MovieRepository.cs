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

        public List<Movie> FilterFromView()
        public void Update(Movie movie)
        {
            Movie? _movie = _db.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (_movie is not null)
            {
                _db.Movies.Update(movie);
            }
        }
    }
}
