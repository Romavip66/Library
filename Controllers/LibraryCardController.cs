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
    public class LibraryCardController : Controller
    {
        private readonly LibraryContext _context;

        public LibraryCardController(LibraryContext context)
        {
            _context = context;
        }

        // GET: LibraryCard
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Library_Card.Include(l => l.User);
            return View(await libraryContext.ToListAsync());
        }

        // GET: LibraryCard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library_Card = await _context.Library_Card
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library_Card == null)
            {
                return NotFound();
            }

            return View(library_Card);
        }

        // GET: LibraryCard/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: LibraryCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ExpirationDate")] Library_Card library_Card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(library_Card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", library_Card.UserId);
            return View(library_Card);
        }

        // GET: LibraryCard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library_Card = await _context.Library_Card.FindAsync(id);
            if (library_Card == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", library_Card.UserId);
            return View(library_Card);
        }

        // POST: LibraryCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ExpirationDate")] Library_Card library_Card)
        {
            if (id != library_Card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(library_Card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Library_CardExists(library_Card.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", library_Card.UserId);
            return View(library_Card);
        }

        // GET: LibraryCard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library_Card = await _context.Library_Card
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library_Card == null)
            {
                return NotFound();
            }

            return View(library_Card);
        }

        // POST: LibraryCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var library_Card = await _context.Library_Card.FindAsync(id);
            _context.Library_Card.Remove(library_Card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Library_CardExists(int id)
        {
            return _context.Library_Card.Any(e => e.Id == id);
        }
    }
}
