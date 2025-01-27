using MovieStore.Models;
using MovieStore.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Repository
{
    public interface IMovieRepository: ICustomRepository<Movie> 
    {
        List<Person> FilterFromView(MovieCreateModel mcm, string relationship);
        Movie Instantiate(MovieCreateModel mcm, List<Person> Actors, List<Person> Writers);
    }
}
