using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryCourse.Data;
using LibraryCourse.Models;
using LibraryCourse.Services;

namespace LibraryCourse.Controllers
{
    public class HistoryController : Controller
    {
        private readonly HistoryService _historyService;

        public HistoryController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        // GET: History
        public async Task<IActionResult> Index()
        {
            var history = await _historyService.GetHistory();
            return View(history);
        }

        /// <summary>
        /// //////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: History/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _historyService.DetailsHistory(id);
            if (history == null)
            {
                return NotFound();
            }

            return View(history);
        }

        // GET: History/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_historyService.getBooks(), "Id", "Id");
            ViewData["CardId"] = new SelectList(_historyService.getCard(), "Id", "Id");
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
                await _historyService.AddAndSave(history);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_historyService.getBooks(), "Id", "Id", history.BookId);
            ViewData["CardId"] = new SelectList(_historyService.getCard(), "Id", "Id", history.CardId);
            return View(history);
        }

        // GET: History/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _historyService.DetailsHistory(id);
            if (history == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_historyService.getBooks(), "Id", "Id", history.BookId);
            ViewData["CardId"] = new SelectList(_historyService.getCard(), "Id", "Id", history.CardId);
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
                    await _historyService.Update(history);
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
            ViewData["BookId"] = new SelectList(_historyService.getBooks(), "Id", "Id", history.BookId);
            ViewData["CardId"] = new SelectList(_historyService.getCard(), "Id", "Id", history.CardId);
            return View(history);
        }

        // GET: History/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _historyService.DetailsHistory(id);
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
            var history = await _historyService.DetailsHistory(id);
            await _historyService.Delete(history);
            return RedirectToAction(nameof(Index));
        }

        private bool HistoryExists(int id)
        {
            return _historyService.HistoryExis(id);
        }
    }
}
