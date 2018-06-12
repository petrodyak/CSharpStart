using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookStore.Models
{
  //Класс DropCreateDatabaseAlways позволяет при каждом новом запуске заполнять базу данных заново некоторыми начальными данными.
  public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
  {
    protected override void Seed(BookContext db)
    {
      //Используя метод db.Books.Add мы добавляем каждый такой объект в базу данных.
      db.Books.Add(new Book { Name = "Война и мир", Author = "Л. Толстой", Price = 220 });
      db.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев", Price = 180 });
      db.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов", Price = 150 });
      db.Books.Add(new Book { Name = "Book4", Author = "Author4", Price = 222 });

      base.Seed(db);
    }
  }
}