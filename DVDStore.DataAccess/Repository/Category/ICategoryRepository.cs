using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Repository
{
    public interface ICategoryRepository: ICustomRepository<Category>
    {
        Category Get(Expression<Func<Category, bool>> filter);
        void Update(Category category);
    }
}
