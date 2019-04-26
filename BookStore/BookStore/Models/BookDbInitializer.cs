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
            db.Books.Add(new Book { Name = "Book1", Author = "Author1", Price = 110 });
            db.Books.Add(new Book { Name = "Book2", Author = "Author2", Price = 220 });
            db.Books.Add(new Book { Name = "Book3", Author = "Author3", Price = 330 });

            base.Seed(db);
        }
    }
}