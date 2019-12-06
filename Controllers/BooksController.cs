using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryCourse.Data;
using LibraryCourse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LibraryCourse.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;
        const string SessionName = "_Name";
        const string SessionAge = "_Age";
        const string SessionKeyDate = "_Date";

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            

            //TempData
            ViewBag.MyStatus = TempData["status"];
            ViewBag.MyId = TempData["id"];
            ViewBag.MyId2 = TempData["id2"];

            /*TempData.Keep("status");
            TempData.Keep("id");*/
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }
            ViewBag.Name = HttpContext.Session.GetString(SessionName);
            ViewBag.Age = HttpContext.Session.GetInt32(SessionAge);
            ViewBag.Date = HttpContext.Session.Get<DateTime>(SessionKeyDate);
            

            ViewBag.MyStatus = TempData["status"];
            
            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Book_Name,Author_Name")] Books books)
        {
            if (ModelState.IsValid)
            {
                _context.Add(books);
                //TempData
                TempData["status"] = $"Book - '{books.Book_Name}' successfully added";
                TempData["id"] = books.Author_Name;


                //Session
                HttpContext.Session.SetString(SessionName, books.Book_Name);
                HttpContext.Session.SetInt32(SessionAge, 20);
                HttpContext.Session.Set<DateTime>(SessionKeyDate, DateTime.Now);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Book_Name,Author_Name")] Books books)
        {
            if (id != books.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(books);
                    TempData["id2"] = books.Book_Name;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _context.Books.FindAsync(id);
            _context.Books.Remove(books);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

    }
}

public static class SessionExtensions

{

    public static void Set<T>(this ISession session, string key, T value)

    {

        session.SetString(key, JsonConvert.SerializeObject(value));

    }

    public static T Get<T>(this ISession session, string key)

    {

        var value = session.GetString(key);

        return value == null ? default(T) :

        JsonConvert.DeserializeObject<T>(value);

    }

}