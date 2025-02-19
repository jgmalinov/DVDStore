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
        public IActionResult CreateView(MovieViewModel mvm)
        {
            List<Person> people = _unitOfWork.People.GetAll().ToList();
            List<Category> categories = _unitOfWork.Categories.GetAll().ToList();
            ViewData["People"] = people;
            ViewData["Categories"] = categories;
            if (mvm == null)
            {
                return View("Create", new MovieViewModel());
            }
            else
            {
                return View("Create", mvm);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieViewModel mvm)
        {
            if (!ModelState.IsValid)
            {
                List<Person> people = _unitOfWork.People.GetAll().ToList();
                List<Category> categories = _unitOfWork.Categories.GetAll().ToList();
                ViewData["People"] = people;
                ViewData["Categories"] = categories;
                return View(mvm);
            }

            Person director = _unitOfWork.People.Get(p => p.Id == mvm.DirectorId);
            List<Person> Writers = _unitOfWork.Movies.ExtractCheckedPeople(mvm, "Writers");
            List<Person> Actors =  _unitOfWork.Movies.ExtractCheckedPeople(mvm, "Actors");

            Movie movie = _unitOfWork.Movies.InstantiateMovie(mvm, Actors, Writers);                  
            _unitOfWork.Movies.Add(movie);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            List<Person> people = _unitOfWork.People.GetAll().ToList();
            List<Category> categories = _unitOfWork.Categories.GetAll().ToList();
            ViewData["People"] = people;
            ViewData["Categories"] = categories;
            
            var movieToUpdate = _unitOfWork.Movies.Get(m => m.Id == id);
            if (movieToUpdate is null)
            {
                return NotFound();
            }
            MovieViewModel mvm = _unitOfWork.Movies.InstantiateMovieViewModel(movieToUpdate);
            return View(mvm);
        }
        [HttpPost]
        public IActionResult Update(MovieViewModel mvm)
        {
            if (mvm == null || !ModelState.IsValid)
            {
                List<Person> people = _unitOfWork.People.GetAll().ToList();
                List<Category> categories = _unitOfWork.Categories.GetAll().ToList();
                ViewData["People"] = people;
                ViewData["Categories"] = categories;
                return View(mvm);
            }
            for (int i=0; i < mvm.Actors.Count; i++)
            {
                mvm.Actors[i].Person = mvm.Writers[i].Person;
            }
            List<Person> actors = _unitOfWork.Movies.ExtractCheckedPeople(mvm, "Actors");
            List<Person> writers = _unitOfWork.Movies.ExtractCheckedPeople(mvm, "Writers");
            Movie movie = _unitOfWork.Movies.InstantiateMovie(mvm, actors, writers);
            _unitOfWork.Movies.Update(movie);
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

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            _unitOfWork.Movies.Delete(movie);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}