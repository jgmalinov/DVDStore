﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateView(MovieCreateModel movieCreateModel)
        {
            List<Person> people = _unitOfWork.People.GetAll().ToList();
            List<Category> categories = _unitOfWork.Categories.GetAll().ToList();
            ViewData["People"] = people;
            ViewData["Categories"] = categories;
            if (movieCreateModel == null)
            {
                return View("Create", new MovieCreateModel());
            }
            else
            {
                return View("Create", movieCreateModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieCreateModel movieCreateModel)
        {
            // Model State is failing
            if (!ModelState.IsValid)
            {
                List<Person> people = _unitOfWork.People.GetAll().ToList();
                List<Category> categories = _unitOfWork.Categories.GetAll().ToList();
                ViewData["People"] = people;
                ViewData["Categories"] = categories;
                return View(movieCreateModel);
            }

            Person director = _unitOfWork.People.Get(p => p.Id == movieCreateModel.DirectorId);
            List<Person> Writers = _unitOfWork.Movies.FilterFromView(movieCreateModel, "Writers");
            List<Person> Actors =  _unitOfWork.Movies.FilterFromView(movieCreateModel, "Actors");

            Movie movie = _unitOfWork.Movies.Instantiate(movieCreateModel, Actors, Writers);                  
            _unitOfWork.Movies.Add(movie);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var movieToUpdate = _unitOfWork.Movies.Get(m => m.Id ==id);
            if (movieToUpdate is null)
            {
                return NotFound();
            }
            return View(movieToUpdate);
        }
        [HttpPost]
        public IActionResult Update(Movie movie)
        {
            if (movie == null || !ModelState.IsValid)
            {
                return View(movie);
            }
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
