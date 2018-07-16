using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity.Controllers
{
  public class StudentController : Controller
  {
    private SchoolContext db = new SchoolContext();

    // GET: Student
    public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
    {
      ViewBag.CurrentSort = sortOrder;
      ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastName_desc" : "";
      ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
      ViewBag.FirstNameSortParm = sortOrder == "firstName" ? "firstName_desc" : "firstName";
      ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
      //If the search string is changed during paging, the page has to be reset to 1, because the new filter can result in different data to display. 
      //The search string is changed when a value is entered in the text box and the submit button is pressed. In that case, the searchString parameter is not null.
      if (searchString != null)
      {
        page = 1;
      }
      else
      {
        searchString = currentFilter;
      }

      ViewBag.CurrentFilter = searchString;

      var students = from s in db.Students
                     select s;

      //Filter by search string 
      students = StudentFilterBySearchString(searchString, students);

      students = StudentSort(sortOrder, students);

      /*The query is not executed until you convert the IQueryable object into a collection by calling a method such as ToList. 
       *Therefore, this code results in a single query that is not executed until the return View statement.*/
      int pageSize = 3;
      //page ?? 1) means return the value of page if it has a value, or return 1 if page is null.
      int pageNumber = (page ?? 1);
      return View(students.ToPagedList(pageNumber, pageSize));
    }

    private static IQueryable<Student> StudentFilterBySearchString(string searchString, IQueryable<Student> students)
    {
      if (!String.IsNullOrEmpty(searchString))
      {
        students = students.Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString));
      }

      return students;
    }

    private static IQueryable<Student> StudentSort(string sortOrder, IQueryable<Student> students)
    {
      switch (sortOrder)
      {
        case "lastName_desc":
          students = students.OrderByDescending(s => s.LastName);
          break;
        case "Date":
          students = students.OrderBy(s => s.EnrollmentDate);
          break;
        case "date_desc":
          students = students.OrderByDescending(s => s.EnrollmentDate);
          break;
        case "firstName":
          students = students.OrderBy(s => s.FirstMidName);
          break;
        case "firstName_desc":
          students = students.OrderByDescending(s => s.FirstMidName);
          break;
        default:
          students = students.OrderBy(s => s.LastName);
          break;
      }

      return students;
    }

    // GET: Student/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Student student = db.Students.Find(id);
      if (student == null)
      {
        return HttpNotFound();
      }
      return View(student);
    }

    // GET: Student/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Student/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "LastName,FirstMidName,EnrollmentDate,EmailAddress")] Student student)
    {
      try
      {
        if (ModelState.IsValid)
        {
          db.Students.Add(student);
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }
      catch (RetryLimitExceededException )
      {
        //Log the error (uncomment dex variable name and add a line here to write a log.
        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
      }
      return View(student);
    }

    // GET: Student/Edit/5
    [HttpGet]
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Student student = db.Students.Find(id);
      if (student == null)
      {
        return HttpNotFound();
      }
      return View(student);
    }

    // POST: Student/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost, ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public ActionResult Edit()
    {
      int? id = Int32.Parse(Request.Params["id"]);

      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var studentToUpdate = db.Students.Find(id);

      //These changes implement a security best practice to prevent overposting, 
      if (TryUpdateModel(studentToUpdate, "",
       new string[] { "LastName", "FirstMidName", "EnrollmentDate", "EmailAddress" }))
      {
        try
        {
          db.SaveChanges();
          return RedirectToAction("Index");
        }
        catch (RetryLimitExceededException )
        {

          //Log the error (uncomment dex variable name and add a line here to write a log.
          ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

        }
      }
      return View(studentToUpdate);

    }

    // GET: Student/Delete/5
    public ActionResult Delete(int? id, bool? saveChangesError = false)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      if (saveChangesError.GetValueOrDefault())
      {
        ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
      }
      Student student = db.Students.Find(id);
      if (student == null)
      {
        return HttpNotFound();
      }
      return View(student);
    }

    // POST: Student/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {

      try
      {
        Student student = db.Students.Find(id);
        db.Students.Remove(student);
        db.SaveChanges();
      }
      catch (RetryLimitExceededException /* dex */)
      {
        //Log the error (uncomment dex variable name and add a line here to write a log.
        return RedirectToAction("Delete", new { id = id, saveChangesError = true });
      }
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
