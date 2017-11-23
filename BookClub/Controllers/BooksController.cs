using BookClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookClub.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            List<Book> books = new List<Book>();
            List<string> response = new List<string>();

            response = DatabaseConfig.ExecuteReader("SELECT Title, ImagePath FROM Book");
            string[] temp = new string[3];
            foreach (string index in response)
            {
                temp = index.Split(',');
                books.Add(new Book() {Title = temp[0], ImagePath = temp[1] });
            }

            return View(books);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            Console.WriteLine(book);

            DatabaseConfig.ExecuteQuery($"INSERT INTO Book (Title, ImagePath) VALUES('{book.Title}', '{book.ImagePath}')");

            return View(book);
        }

    }
}