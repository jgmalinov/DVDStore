using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Repository
{
    public interface IMovieRepository: IRepository<Movie>
    {
        void Update(Movie movie);
    }
}
