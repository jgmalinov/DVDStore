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
                TempData["success"] = "Genre created successfully!";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            //Category? category1 = _db.Categories.FirstOrDefault(g => g.Id == id);
            //Category? category2 = _db.Categories.Where(g => g.Id == id).FirstOrDefault();
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Genre updated successfully!";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            
            Category? category = _db.Categories.Find(id);

            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? objToRemove = _db.Categories.FirstOrDefault(cat => cat.Id == id);
            if (objToRemove is null)
            {
                return NotFound();
            }

            _db.Categories.Remove(objToRemove);
            _db.SaveChanges();
            TempData["success"] = "Genre deleted successfully!";
            return RedirectToAction("Index", "Category");
        }
    }
}
