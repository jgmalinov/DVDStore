using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DataAccess.Data;
using MovieStore.Models;


namespace MovieStore.DataAccess.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext db) : base(db) { }
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
