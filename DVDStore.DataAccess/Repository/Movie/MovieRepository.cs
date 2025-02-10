using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.DataAccess.Data;
using MovieStore.Models;


namespace MovieStore.DataAccess.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext db) : base(db) { }
        public Movie? Get(Expression<Func<Movie, bool>> exp)
        {
            Movie? movie = _db.Movies.FirstOrDefault(exp);
            _db.Entry(movie).Reference(m => m.Director).Load();
            _db.Entry(movie).Collection(m => m.Writers).Load();
            _db.Entry(movie).Collection(m => m.Actors).Load();
            return movie;
        }

        public List<Movie> Filter(Expression<Func<Movie, bool>> filter)
        {
            List<Movie> movies =_db.Movies.Where(filter).ToList();
            foreach(var movie in movies)
            {
                _db.Entry(movie).Reference(m => m.Director).Load();
                _db.Entry(movie).Collection(m => m.Writers).Load();
                _db.Entry(movie).Collection(m => m.Actors).Load();
            }
            return movies;
        }
        public List<Person> ExtractCheckedPeople(MovieViewModel mvm, string relationship)
        {
            List<Person> people = new List<Person>();
            switch(relationship)
            {
                case "Actors":
                    people = mvm.Actors.Where(a => a.isChecked).Select(a => a.Person).ToList();
                    break;
                case "Writers":
                    people = mvm.Writers.Where(a => a.isChecked).Select(a => a.Person).ToList();
                    break;
            }
            return people;
        }

        public Movie InstantiateMovie(MovieViewModel mcm, List<Person> Actors, List<Person> Writers)
        {
            Movie movie = new Movie()
            {
                Id = mcm.Id,
                Title = mcm.Title,
                ReleaseDate = mcm.ReleaseDate,
                Price = mcm.Price,
                Price5 = mcm.Price5,
                Price10 = mcm.Price10,
                Summary = mcm.Summary,
                CategoryId = mcm.CategoryId,
                DirectorId = mcm.DirectorId,
            };
            
            return movie;
        }
        public MovieViewModel InstantiateMovieViewModel(Movie movie)
        {
            MovieViewModel mvm = new MovieViewModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Price = movie.Price,
                Price5 = movie.Price5,
                Price10 = movie.Price10,
                Summary = movie.Summary,
                CategoryId = movie.CategoryId,
                DirectorId = movie.DirectorId,
            };

            var i=0;
            foreach (var person in _db.People.ToList())
            {
                mvm.Actors.Add(new PersonCheckModel() {Person = person, isChecked = movie.Actors.Any(a => a.Id == person.Id ? true : false)});
                mvm.Writers.Add(new PersonCheckModel() {Person = person, isChecked = movie.Writers.Any(a => a.Id == person.Id ? true : false)});
                i++;
            }
            return mvm;
        }
        public void Update(Movie movie)
        /*
        {
            // Duplicate Person tracking most likely caused by adding the same person once as a writer and then as an actor.
            Movie? _movie = _db.Movies
                            .Include(m => m.Actors)
                            .Include(m => m.Writers)
                            .FirstOrDefault(m => m.Id == movie.Id);
            
            // Obtain entities from tracker
            var trackedEntities = _db.ChangeTracker.Entries();
            List<Person> trackedPeople = trackedEntities
                                        .Where(e => e.Entity.GetType() == typeof(Person))
                                        .Select(e => (Person) e.Entity).ToList();
            foreach (var entry in trackedEntities)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }

            if (_movie is not null)
            {
                _db.Entry(_movie).CurrentValues.SetValues(movie);
                foreach(Person actor in movie.Actors)
                {
                    Person? alreadyTrackedPerson = trackedPeople.FirstOrDefault(p => p.Id == actor.Id);
                    if (!_movie.Actors.Any(a => a.Id == actor.Id))
                    {
                        if (alreadyTrackedPerson is not null)
                        {
                            _movie.Actors.Add(alreadyTrackedPerson);
                        }
                        else
                        {
                            _movie.Actors.Add(actor);
                        }
                    }
                }
                foreach(Person writer in movie.Writers)
                {
                    Person? alreadyTrackedPerson = trackedPeople.FirstOrDefault(p => p.Id == writer.Id);
                    if (!_movie.Writers.Any(a => a.Id == writer.Id))
                    {
                        if (alreadyTrackedPerson is not null)
                        {
                            _movie.Writers.Add(alreadyTrackedPerson);
                        } else 
                        {
                            _movie.Writers.Add(writer);
                        }
                    }
                }
            }
            foreach (var entry in trackedEntities)
            trackedEntities = _db.ChangeTracker.Entries();
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }
        }

        _db.Entry(_movie).Collection<Person>("Actors").Load();
        _db.Entry(_movie).Collection<Person>("Writers").Load();
        List<Person>  = _movie.Actors.Union(movie.Actors).Intersect(movie.Actors).ToList();
            _movie.Writers = _movie.Writers.Union(movie.Writers).Intersect(movie.Writers).ToList();
            var AllPeopleInvoled = _movie.Actors.Union(_movie.Writers);
            foreach(var person in AllPeopleInvoled)
            {
                _db.People.Attach(person);
                _db.Entry(person).State = EntityState.Modified;
            }
        */
        {
            Movie? _movie = _db.Movies
                            .Include(m => m.Actors)
                            .Include(m => m.Writers)
                            .FirstOrDefault(m => m.Id == movie.Id);
            _db.Entry(_movie).CurrentValues.SetValues(movie);
            Dictionary<Person, String> actorsToModify = new Dictionary<Person, string>();
            foreach(var person in _movie.Actors)
            {
                if (!movie.Actors.Any(p => p.Id == person.Id))
                {
                    actorsToModify.Add(person, "Remove");
                }
            }
            var trackedEntities = _db.ChangeTracker.Entries();
            List<Person> trackedPeople = _db.ChangeTracker.Entries()
                                        .Where(e => e.Entity.GetType() == typeof(Person))
                                        .Select(e => (Person) e.Entity)
                                        .ToList();
            foreach (var entry in trackedEntities)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }
            foreach(var person in movie.Actors)
            {
                if (!_movie.Actors.Any(p => p.Id == person.Id))
                {
                    actorsToModify.Add(person, "Add");
                }
            }

            foreach(var actor in actorsToModify)
            {
                Person actorTrackerChecked = trackedPeople.FirstOrDefault(p => p.Id == actor.Key.Id);
                var result = actorTrackerChecked != null ? actorTrackerChecked : actor.Key;
                if(actor.Value == "Add")
                {
                    _movie.Actors.Add(result);
                } else
                {
                    _movie.Actors.Remove(result);
                }
            }
            trackedEntities = _db.ChangeTracker.Entries();
            foreach (var entry in trackedEntities)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }
            Dictionary<Person, String> writersToModify = new Dictionary<Person, string>();
            foreach(var person in _movie.Writers)
            {
                if (!movie.Writers.Any(p => p.Id == person.Id))
                {
                    writersToModify.Add(person, "Remove");
                }
            }
            foreach(var person in movie.Writers)
            {
                if (!_movie.Writers.Any(p => p.Id == person.Id))
                {
                    writersToModify.Add(person, "Add");
                }
            }
            trackedEntities = _db.ChangeTracker.Entries();
            foreach (var entry in trackedEntities)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }
            foreach(var writer in writersToModify)
            {
                Person writerTrackerChecked = trackedPeople.FirstOrDefault(p => p.Id == writer.Key.Id);
                var result = writerTrackerChecked != null ? writerTrackerChecked : writer.Key;
                if(writer.Value == "Add")
                {
                    _movie.Writers.Add(result);
                } else
                {
                    _movie.Writers.Remove(result);
                }
            }
        }
    }
}
