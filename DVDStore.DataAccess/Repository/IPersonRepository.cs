using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieStore.Models;

namespace MovieStore.DataAccess.Repository
{
    public interface IPersonRepository: IRepository<Person>
    {
        Person Get(Expression<Func<Person, bool>> filter);
        public void Update(Person person);
    }
}
