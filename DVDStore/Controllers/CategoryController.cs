using DVDStore.DataAccess.Data;
using DVDStore.DataAccess.Repository;
using DVDStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DVDStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
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
            Category? category = _categoryRepo.Get(c => c.Id == id);
            //Category? category1 = _categoryRepo.Categories.FirstOrDefault(g => g.Id == id);
            //Category? category2 = _categoryRepo.Categories.Where(g => g.Id == id).FirstOrDefault();
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
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
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
            
            Category? category = _categoryRepo.Get(c => c.Id == id);

            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? objToRemove = _categoryRepo.Get(c => c.Id == id);
            if (objToRemove is null)
            {
                return NotFound();
            }

            _categoryRepo.Delete(objToRemove);
            _categoryRepo.Save();
            TempData["success"] = "Genre deleted successfully!";
            return RedirectToAction("Index", "Category");
        }
    }
}
