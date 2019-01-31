using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUD_Core_Ajax.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Core_Ajax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Fetching Data for AJAX Call
        public IActionResult GetData()
        {
            List<Book> books;
            using (_db)
            {
                books = _db.Books.ToList();
            }
            return new JsonResult(books);
        }

        // GET: Delete Data from AJAX Call
        public async Task<IActionResult> DeleteData(int bookId)
        {
            Book book;
            using (_db)
            {
                book = await _db.Books.FindAsync(bookId);
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();
            }

            return new JsonResult(book);
        }

        [HttpPost]
        // POST: Adding Data with AJAX
        public async Task<IActionResult> AddData([FromBody]Book book)
        {
            using (_db)
            {
                if (book != null)
                {
                    await _db.Books.AddAsync(book);
                    _db.SaveChanges();
                    return new JsonResult(book);
                }
            }
            return new JsonResult(null);
        }

        [HttpPost]
        // POST: Adding Multiple Data with AJAX
        public JsonResult AddMultipleData([FromBody] string[][] booksArray)
        {
            if (booksArray != null && booksArray.Length > 0)
            {
                List<Book> books = new List<Book>();
                for (int i = 0; i < booksArray.Length; i++)
                {
                    Book stu = new Book
                    {
                        Name = booksArray[i][0],
                        ISBN = booksArray[i][1],
                        Author = booksArray[i][2]
                    };
                    books.Add(stu);
                    books.TrimExcess();
                }

                List<Book> oldBooksList;

                using (_db)
                {
                    oldBooksList = (from s in _db.Books select s).ToList();
                    books.RemoveAll(x => oldBooksList.Exists(y => y.Name == x.Name || y.ISBN == x.ISBN));
                    _db.Books.AddRange(books);
                    _db.SaveChanges();
                }

                return new JsonResult(new { success = true, responseText = "New Data added to Database Successfully!" });
            }
            return new JsonResult(new { success = false, responseText = "Something Went Wrong!" });
        }

        [HttpPost]
        // POST: Updateing Data with AJAX
        public async Task<IActionResult> UpdateData([FromBody] Book book)
        {
            Book oldBook = _db.Books.Find(book.Id);

            if (oldBook != null)
            {
                oldBook.Name = book.Name;
                oldBook.ISBN = book.ISBN;
                oldBook.Author = book.Author;
                using (_db)
                {
                    _db.Entry(oldBook).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                }
                return new JsonResult(book);
            }
            return new JsonResult("error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
