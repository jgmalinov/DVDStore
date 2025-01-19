using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DataAccess.Data;
using MovieStore.Models;

namespace MovieStore.DataAccess.Repository
{
    public class PersonRepository: Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext db): base(db) { }
        public void Update(Person person)
        {
            _db.People.Update(person);
        }
    }
}
