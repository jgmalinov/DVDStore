using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Models;

namespace MovieStore.DataAccess.Repository
{
    public interface IPersonRepository: ICustomRepository<Person>
    {
        Person Get(Expression<Func<Person, bool>> filter);
        List<Movie> Filter(Expression<Func<Person, bool>> filter);
        void Update(Person person);
    }
}
