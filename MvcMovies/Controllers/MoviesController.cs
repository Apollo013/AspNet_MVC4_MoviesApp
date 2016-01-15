using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovies.Contexts;
using MvcMovies.Models;

namespace MvcMovie.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        //
        // GET: /Movies/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        //
        // GET: /Movies/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id = 0)
        {

            Movie movie = db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Movies/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        //
        // GET: /Movies/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //
        // GET: /Movies/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult SearchIndex(string movieGenre, string searchString)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (string.IsNullOrEmpty(movieGenre))
                return View(movies);
            else
            {
                return View(movies.Where(x => x.Genre == movieGenre));
            }
        }


/**********************************************************************************************************************************/

        [AllowAnonymous]
        public PartialViewResult GenreMenu(string genre = null)
        {
            ViewBag.SelectedGenre = genre;

            List<string> genreslist = new List<string>();

            var genresQry = from g in db.Movies
                            orderby g.Genre
                            select g.Genre;

            genreslist.AddRange(genresQry.Distinct());

            return PartialView(genreslist);
        }

        [AllowAnonymous]
        public ActionResult SearchGenre(string movieGenre = null)
        {
            ViewBag.SelectedGenre = movieGenre;

            List<Movie> movieList = new List<Movie>();

            var movies = from m in db.Movies
                         select m;

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movieList.AddRange(movies.Where(x => x.Genre == movieGenre));
            }
        
            return View(movieList);
        }

/**********************************************************************************************************************************/

        [AllowAnonymous]
        public PartialViewResult ActorMenu(string actor = null)
        {
            ViewBag.SelectedActor = actor;

            List<string> actorslist = new List<string>();

            var actorQry = from a in db.MovieStarz
                               orderby a.name
                               select a.name;

            actorslist.AddRange(actorQry.Distinct());

            return PartialView(actorslist);
        }

        [AllowAnonymous]
        public ActionResult SearchActorMovies(string actor)
        {

            ViewBag.SelectedActor = actor;

            List<Movie> movieList = new List<Movie>();

            if (!String.IsNullOrEmpty(actor))
            {
                var movies = (from m in db.Movies
                              from ms in m.MovieStars
                              where ms.name.Contains(actor)
                              select m);

                movieList.AddRange(movies);
            }

            return View(movieList);
        }

/**********************************************************************************************************************************/



                /**********************************************************************************************************************************/

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}