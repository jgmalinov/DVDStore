using MovieStore.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IMovieRepository Movie { get; private set; }
        public IPersonRepository Person { get; private set; }
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Movie = new MovieRepository(_db);
            Person = new PersonRepository(_db);
        }
        public void Save() { _db.SaveChanges(); }
    }
}
