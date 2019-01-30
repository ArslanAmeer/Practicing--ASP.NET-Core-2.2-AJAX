using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUD_Core_Ajax.Models;

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
