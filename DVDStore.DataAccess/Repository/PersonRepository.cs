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
    public class PersonRepository: Repository<Person>, IPersonRepository
    {
        public Person? Get(Expression<Func<Person, bool>> exp)
        {
            Person? person = _db.People.FirstOrDefault(exp);
            _db.Entry(person).Collection(p => p.MoviesWrittenFor).Load();
            _db.Entry(person).Collection(p => p.MoviesDirected).Load();
            _db.Entry(person).Collection(p => p.MoviesStarredIn).Load();
            return person;
        }
        public PersonRepository(ApplicationDbContext db): base(db) { }
        public void Update(Person person)
        {
            _db.People.Update(person);
        }
    }
}
