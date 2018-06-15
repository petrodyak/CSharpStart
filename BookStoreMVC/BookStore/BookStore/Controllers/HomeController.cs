using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
  public class HomeController : Controller
  {
    // создаем контекст данных
    BookContext db = new BookContext();

    //Это метод  действий или просто действия контроллера
    public ActionResult Index()
    {
      // получаем из бд все объекты Book      
      IEnumerable<Book> books = db.Books;
      // передаем все объекты в динамическое свойство Books в ViewBag
      ViewBag.Books = books;
      ViewBag.Triangle = new Triangle();
      // возвращаем представление
      return View();
    }


    [HttpGet]
    public ActionResult Buy(int? id)
    {
      ViewBag.BookId = id;
      return View();
    }

    [HttpPost]
    public string Buy(Purchase purchase)
    {
      //Обратите внимание на поля ввода <input type="text" name="Person" /> и <input type="text" name="Address" />. 
      //Значение их атрибута name соответствуют именам свойств модели Purchase. 

      purchase.Date = getToday();
      // добавляем информацию о покупке в базу данных
      db.Purchases.Add(purchase);
      // сохраняем в бд все изменения
      //принимает переданную ему в запросе POST модель purchase и добавляет ее в базу данных.
      db.SaveChanges();
      return "Спасибо," + purchase.Person + ", за покупку!";
    }

    private DateTime getToday()
    {
      return DateTime.Now;
    }
  }
}