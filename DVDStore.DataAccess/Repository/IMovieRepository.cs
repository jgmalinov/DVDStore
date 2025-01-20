using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Repository
{
    public interface IMovieRepository: IRepository<Movie>
    {
        Movie Get(Expression<Func<Movie, bool>> filter);
        void Update(Movie movie);
    }
}
