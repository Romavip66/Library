using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryCourse.Data;
using LibraryCourse.Models;

namespace LibraryCourse.Controllers
{
    public class HistoryController : Controller
    {
        private readonly LibraryContext _context;

        public HistoryController(LibraryContext context)
        {
            _context = context;
        }

        // GET: History
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.History.Include(h => h.Books).Include(h => h.Library_Card);
            return View(await libraryContext.ToListAsync());
        }

        // GET: History/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _context.History
                .Include(h => h.Books)
                .Include(h => h.Library_Card)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (history == null)
            {
                return NotFound();
            }

            return View(history);
        }

        // GET: History/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["CardId"] = new SelectList(_context.Library_Card, "Id", "Id");
            return View();
        }

        // POST: History/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardId,BookId")] History history)
        {
            if (ModelState.IsValid)
            {
                _context.Add(history);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", history.BookId);
            ViewData["CardId"] = new SelectList(_context.Library_Card, "Id", "Id", history.CardId);
            return View(history);
        }

        // GET: History/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _context.History.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", history.BookId);
            ViewData["CardId"] = new SelectList(_context.Library_Card, "Id", "Id", history.CardId);
            return View(history);
        }

        // POST: History/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CardId,BookId")] History history)
        {
            if (id != history.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(history);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoryExists(history.CardId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", history.BookId);
            ViewData["CardId"] = new SelectList(_context.Library_Card, "Id", "Id", history.CardId);
            return View(history);
        }

        // GET: History/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _context.History
                .Include(h => h.Books)
                .Include(h => h.Library_Card)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (history == null)
            {
                return NotFound();
            }

            return View(history);
        }

        // POST: History/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var history = await _context.History.FindAsync(id);
            _context.History.Remove(history);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoryExists(int id)
        {
            return _context.History.Any(e => e.CardId == id);
        }
    }
}
