using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pustok_MVC.Areas.Manage.ViewModels;
using Pustok_MVC.Data;
using Pustok_MVC.Models;

namespace Pustok_MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Books.Include(x => x.Author).Include(x => x.Genre).Include(x => x.BookImages.Where(x => x.PosterStatus == true)).OrderByDescending(x => x.Id);

            var data = PaginatedList<Book>.Create(query, page, 2);
            if (data.TotalPages < page) return RedirectToAction("index", new { page = data.TotalPages });
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();

                return View(book);
            }

            if (!_context.Authors.Any(x => x.Id == book.AuthorId))
                return RedirectToAction("NotFound", "Error");

            if (!_context.Genres.Any(x => x.Id == book.GenreId))
                return RedirectToAction("NotFound", "Error");

            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Book book = _context.Books.Find(id);

            if (book == null)
                return RedirectToAction("NotFound", "Error");

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            Book? existBook = _context.Books.Find(book.Id);
            if (existBook == null) 
                return RedirectToAction("NotFound", "Error");


            if (book.AuthorId != existBook.AuthorId && !_context.Authors.Any(x => x.Id == book.AuthorId))
                return RedirectToAction("NotFound", "Error");

            if (book.GenreId != existBook.GenreId && !_context.Genres.Any(x => x.Id == book.GenreId))
                return RedirectToAction("NotFound", "Error");

            existBook.Name = book.Name;
            existBook.Desc = book.Desc;
            existBook.SalePrice = book.SalePrice;
            existBook.CostPrice = book.CostPrice;
            existBook.DiscountPercent = book.DiscountPercent;
            existBook.IsNew = book.IsNew;
            existBook.IsFeatured = book.IsFeatured;
            existBook.StockStatus = book.StockStatus;

            existBook.ModifiedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}