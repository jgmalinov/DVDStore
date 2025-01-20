using MovieStore.DataAccess.Data;
using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Repository
{
    public class CategoryRepository: Repository.Repository<Category>, ICategoryRepository 
    {
        public CategoryRepository(ApplicationDbContext db) : base(db) { } 
        public Category? Get(Expression<Func<Category, bool>> exp)
        {
            Category? cat = _db.Categories.FirstOrDefault(exp);
            return cat;
        }
        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
