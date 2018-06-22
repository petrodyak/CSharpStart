using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
  public class MoviesController : Controller
  {
    private MovieDBContext db = new MovieDBContext();

    // GET: Movies
    public ActionResult Index(string movieGenre, string searchString)
    {

      var GenreList = new List<string>();

      var GenreQuery = from d in db.Movies
                       orderby d.Genre
                       select d.Genre;

      //The code uses the AddRange method of the generic List collection to add all the distinct genres to the list
      //Storing category data (such a movie genre's) as a SelectList object in a ViewBag, then accessing the category data in a dropdown list box is a typical approach for MVC applications.
      GenreList.AddRange(GenreQuery.Distinct());
      ViewBag.movieGenre = new SelectList(GenreList);

      //The first line of the Index method creates the following LINQ query to select the movies:
      var movies = from m in db.Movies
                   select m;
      //Console.WriteLine("searchString: " + searchString);

      //If the searchString parameter contains a string, the movies query is modified to filter on the value of the search string, using the following code:
      if (!String.IsNullOrEmpty(searchString))
      {
        movies = movies.Where(s => s.Title.Contains(searchString));
      }

      if (!string.IsNullOrEmpty(movieGenre))
      {
        movies = movies.Where(x => x.Genre == movieGenre);
      }

      return View(movies);
    }

    // GET: Movies/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Movie movie = db.Movies.Find(id);
      if (movie == null)
      {
        return HttpNotFound();
      }
      return View(movie);
    }

    // GET: Movies/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Movies/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
    {
      //calls ModelState.IsValid to check whether the movie has any validation errors. 
      if (ModelState.IsValid)
      {
        db.Movies.Add(movie);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(movie);
    }

    // GET: Movies/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Movie movie = db.Movies.Find(id);
      if (movie == null)
      {
        return HttpNotFound();
      }
      return View(movie);
    }

    // POST: Movies/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //This attribute [HttpPost] specifies that the overload of the Edit method can be invoked only for POST requests.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
    {
      if (ModelState.IsValid)
      {
        db.Entry(movie).State = EntityState.Modified;
        db.SaveChanges();
        // After saving the data, the code redirects the user to the Index action method of the MoviesController class, 
        //which displays the movie collection, including the changes just made.
        return RedirectToAction("Index");
      }
      return View(movie);
    }

    //here you need two Delete methods -- one for GET and one for POST -- that both have the same parameter signature. 
    //(They both need to accept a single integer as a parameter.)

    // GET: Movies/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Movie movie = db.Movies.Find(id);
      if (movie == null)
      {
        return HttpNotFound();
      }
      return View(movie);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    //Note that the HTTP GET Delete method doesn't delete the specified movie, it returns a view of the movie where you can submit (HttpPost) the deletion
    //the routing system so that a URL that includes /Delete/ for a POST request will find the DeleteConfirmed method
    public ActionResult DeleteConfirmed(int id)
    {
      Movie movie = db.Movies.Find(id);
      db.Movies.Remove(movie);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
