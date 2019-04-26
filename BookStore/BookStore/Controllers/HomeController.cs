using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        //Create Context of data
        //Затем создается объект контекста данных, через который мы будем взаимодействовать с бд
        BookContext db = new BookContext(); 

        public ActionResult Index()
        {
            //get from DB all Book Objects
            IEnumerable<Book> books = db.Books;

            //Send all objects inot dymanic property Books in ViewBag
            //Для передачи списка объектов Book в представление используем объект ViewBag.
            ViewBag.Books = books;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}