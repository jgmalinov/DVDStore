using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MovieStore.DataAccess.Repository;
using MovieStore.Models;

namespace MovieStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private IUnitOfWork _unitOfWork { get; set; }
        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Movie> Movies = _unitOfWork.Movies.GetAll().ToList();
            return View(Movies);
        }

        public IActionResult Get(int id)
        {
            Movie movieToGet = _unitOfWork.Movies.Get(m => m.Id == id);
            if (movieToGet is null)
            {
                return NotFound();
            }
            return View(movieToGet);
        }
        [HttpGet]
        public IActionResult Upsert(int? Id)
        {
            List<Category> categories = _unitOfWork.Categories.GetAll().ToList();
            TempData["Categories"] = categories;
            Movie? movie = _unitOfWork.Movies.Get(m => m.Id == Id);
            MovieViewModel mvm = _unitOfWork.Movies.InstantiateMovieViewModel(movie);
            return View("Upsert", mvm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(MovieViewModel mvm, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                return View(mvm);
            }

            List<Person> Writers = _unitOfWork.Movies.ExtractCheckedPeople(mvm, "Writers");
            List<Person> Actors =  _unitOfWork.Movies.ExtractCheckedPeople(mvm, "Actors");

            Movie movie = _unitOfWork.Movies.InstantiateMovie(mvm, Actors, Writers);
            bool isCreate = _unitOfWork.Movies.IsCreate(movie);
            if (isCreate)
            {
                _unitOfWork.Movies.Add(movie);
            }
            else 
            {
                _unitOfWork.Movies.Update(movie);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var movieToDelete = _unitOfWork.Movies.Get(m => m.Id == id);
            if (movieToDelete is null)
            {
                return NotFound();
            } else
            {
                _unitOfWork.Movies.Delete(movieToDelete);
            }
            return View(movieToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int Id)
        {
            Movie _movie = _unitOfWork.Movies.Get(m => m.Id == Id);
            _unitOfWork.Movies.Delete(_movie);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}