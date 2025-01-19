using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IMovieRepository Movies { get; }
        IPersonRepository People { get; }
        void Save();
    }
}
