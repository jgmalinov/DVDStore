using DVDStore.Data;
using DVDStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DVDStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", "Name and display order cannot be of equal values.");
            }
            //if (obj.Name is not null && obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "'Test' is an invalid genre value.");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }
        }
    }
}
