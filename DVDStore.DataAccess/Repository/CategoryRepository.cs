using DVDStore.DataAccess.Data;
using DVDStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDStore.DataAccess.Repository
{
    public class CategoryRepository: Repository.Repository<Category>, ICategoryRepository 
    {
        public CategoryRepository(ApplicationDbContext db) : base(db) { }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
