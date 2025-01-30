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
        public ICategoryRepository Categories { get; private set; }
        public IMovieRepository Movies { get; private set; }
        public IPersonRepository People { get; private set; }
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Categories = new CategoryRepository(_db);
            Movies = new MovieRepository(_db);
            People = new PersonRepository(_db);
        }
        public void Save() 
        { 
            _db.SaveChanges();
        }
    }
}
