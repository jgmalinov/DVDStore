using Microsoft.AspNetCore.Mvc;
using MovieStore.DataAccess.Repository;
using MovieStore.Models;

namespace MovieStore.Areas.Admin.Controllers
{
    public class PersonController : Controller
    {
        private IUnitOfWork _unitOfWork { get; set; }
        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Person> People = _unitOfWork.People.GetAll().ToList();
            return View(People);
        }

        public IActionResult Get(int id)
        {
            Person PersonToGet = _unitOfWork.People.Get(m => m.Id == id);
            if (PersonToGet is null)
            {
                return NotFound();
            }
            return View(PersonToGet);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person Person)
        {
            if (Person == null || !ModelState.IsValid)
            {
                return View(Person);
            }
            _unitOfWork.People.Add(Person);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var PersonToUpdate = _unitOfWork.People.Get(m => m.Id == id);
            if (PersonToUpdate is null)
            {
                return NotFound();
            }
            return View(PersonToUpdate);
        }
        [HttpPost]
        public IActionResult Update(Person Person)
        {
            if (Person == null || !ModelState.IsValid)
            {
                return View(Person);
            }
            _unitOfWork.People.Update(Person);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var PersonToDelete = _unitOfWork.People.Get(m => m.Id == id);
            if (PersonToDelete is null)
            {
                return NotFound();
            }
            return View(PersonToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Person Person)
        {
            _unitOfWork.People.Delete(Person);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
