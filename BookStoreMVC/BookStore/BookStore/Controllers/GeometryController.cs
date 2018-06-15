using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class GeometryController : Controller
    {
        // GET: Geometry/Index
    public ActionResult Index()
    {
      var model = new Triangle();
      //triangle.Side = 10;
      //triangle.Height = 15;
      return View(model);
      //return "This is my <b>default</b> action...";
    }

    // 
    // GET: /Geometry/Welcome/ 

    public string Welcome()
    {
      return "This is the Welcome action method...";
    }
    /// <summary>
    /// Подсчет прощади треугольника. Этот метод должен вызываться когда 
    /// параметры передаються напрямую через строку, а не методом ввода через форму
    /// </summary>
    /// <param name="a">сторона треугольника</param>
    /// <param name="h">высота треугольника</param>
    /// <returns></returns>
    [HttpPost]
    public string Square(Triangle triangle)
    {
      int a = triangle.Side;
      int h = triangle.Height;

      double s = a * h / 2.0;
      return "<h2>Площадь треугольника с основанием " + a +
              " и высотой " + h + " равна " + s + "</h2>";

    }

    /// <summary>
    ////Этот метод ложен вызываться когда появляется форма
    /// </summary>
    /// <returns></returns>
    //public ActionResult Square()
    //{
    //  Triangle triangle = new Triangle();
    //  int a;
    //  int h;

    //  if (Request.Params["a"] == null)
    //    a = (Int32)2;
    //  else
    //    a = Int32.Parse(Request.Params["a"]);

    //  if (Request.Params["h"] == null)
    //    h = (Int32)1;
    //  else
    //    h = Int32.Parse(Request.Params["h"]);

    //  //int h = Int32.Parse(Request.Params["h"]);

    //  triangle.Height = h;
    //  triangle.Side = a;
    //  return View(Square(triangle));
    //}

  }
}