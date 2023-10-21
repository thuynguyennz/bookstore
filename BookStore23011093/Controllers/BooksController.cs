using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BookStore23011093.Data;
using BookStore23011093.Models;
using BookStore23011093.ViewModels;
using PagedList;
using static BookStore23011093.ViewModels.BookIndexViewModel;

namespace BookStore23011093.Controllers
{
    public class BooksController : Controller
    {
        private BookStore23011093Context db = new BookStore23011093Context();

        // GET: Books
        public async Task<ActionResult> Index(string Genre, string search, string sortOrder, int? page )
        {
            BookIndexViewModel viewModel = new BookIndexViewModel();
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GenreSortParm = string.IsNullOrEmpty(sortOrder) ? "genre_desc" : "genre";
            var books = db.Books.Include(b => b.Genre);
       
            //find the books where either the book title field contains search,the book
            //description contains search, or the book's category name contains search

            if (!string.IsNullOrEmpty(search))
            {
                var searchTerm = search.Trim().ToLower();
                books = books.Where(b => b.Title.ToLower().Contains(searchTerm) ||
                                         b.Description.ToLower().Contains(searchTerm) ||
                                         b.Genre.Name.ToLower().Contains(searchTerm));
                viewModel.Search = search;
                page = 1;
            }
           
            viewModel.GenresWithCount = from matchingBooks in books
                                      where
                                      matchingBooks.GenreID != null
                                      group matchingBooks by
                                      matchingBooks.Genre.Name into
                                      catGroup
                                      select new GenreWithCount()
                                      {
                                          GenreName = catGroup.Key,
                                          BookCount = catGroup.Count()
                                      };


            //var categories = books.OrderBy(b => b.Category.Name).Select(b=> b.Category.Name).Distinct();

            if (!string.IsNullOrEmpty(Genre))
            {
                books = books.Where(b => b.Genre.Name == Genre);
            }
            //ViewBag.Category = new SelectList(categories);
            //return View(books.ToList());
            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "genre_desc":
                    books = books.OrderByDescending(b => b.Genre.Name);
                    break; 
                case "genre":
                    books = books.OrderBy(b => b.Genre.Name);
                    break;  
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }
          
          
           int pageSize = 3;
           int pageNumber = (page ?? 1);
           // viewModel.Books = books;
           viewModel.Books = books.ToPagedList(pageNumber, pageSize);
            //return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
            return View(viewModel);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.GerneId = new SelectList(db.Genres, "ID", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Language,ISBN,DatePublished,Price,Author,GerneId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GerneId = new SelectList(db.Genres, "ID", "Name", book.GenreID);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.GerneId = new SelectList(db.Genres, "ID", "Name", book.GenreID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Language,ISBN,DatePublished,Price,Author,GerneId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GerneId = new SelectList(db.Genres, "ID", "Name", book.GenreID);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
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
