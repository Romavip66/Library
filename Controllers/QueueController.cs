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

namespace LibraryCourse.Controllers
{
    [Authorize]
    public class QueueController : Controller
    {
        private readonly LibraryContext _context;

        public QueueController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Queue
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Queue.Include(q => q.Books).Include(q => q.Library_Card);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Queue/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queue
                .Include(q => q.Books)
                .Include(q => q.Library_Card)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // GET: Queue/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["CardId"] = new SelectList(_context.Library_Card, "Id", "Id");
            return View();
        }

        // POST: Queue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardId,BookId,StartTime,EndTime")] Queue queue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(queue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", queue.BookId);
            ViewData["CardId"] = new SelectList(_context.Library_Card, "Id", "Id", queue.CardId);
            return View(queue);
        }

        // GET: Queue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queue.FindAsync(id);
            if (queue == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", queue.BookId);
            ViewData["CardId"] = new SelectList(_context.Library_Card, "Id", "Id", queue.CardId);
            return View(queue);
        }

        // POST: Queue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CardId,BookId,StartTime,EndTime")] Queue queue)
        {
            if (id != queue.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(queue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueueExists(queue.CardId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", queue.BookId);
            ViewData["CardId"] = new SelectList(_context.Library_Card, "Id", "Id", queue.CardId);
            return View(queue);
        }

        // GET: Queue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queue = await _context.Queue
                .Include(q => q.Books)
                .Include(q => q.Library_Card)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // POST: Queue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queue = await _context.Queue.FindAsync(id);
            _context.Queue.Remove(queue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueueExists(int id)
        {
            return _context.Queue.Any(e => e.CardId == id);
        }
    }
}
