using LibraryCourse.Abstractions;
using LibraryCourse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Services
{
    public class HistoryService
    {
        private readonly IHistoryR _historyRepo;
        public HistoryService(IHistoryR historyRepo)
        {
            _historyRepo = historyRepo;
        }

        public async Task<List<History>> GetHistory()
        {
            return await _historyRepo.GetAll();
        }
        
        public async Task<History> DetailsHistory(int? id)
        {
            return await _historyRepo.GetDetail(id);
            //return await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);
        }
        // For last method
        public bool HistoryExis(int id)
        {
            return _historyRepo.Exist(id);
            //return _context.Roles.Any(m => m.Id == id);
        }
        // POST: Roles/Create
        public async Task AddAndSave(History history)
        {
            _historyRepo.Add(history);
            await _historyRepo.Save();
        }

        // POST: Roles/Edit/5
        public async Task Update(History history)
        {
            _historyRepo.Update(history);
            await _historyRepo.Save();
            //_context.Roles.Update(role);
            //await _context.SaveChangesAsync();
        }

        // POST: Roles/Delete/5
        public async Task Delete(History history)
        {
            _historyRepo.Delete(history);
            await _historyRepo.Save();
            //_context.Roles.Remove(role);
            //await _context.SaveChangesAsync();
        }

        public DbSet<Library_Card> getCard()
        {
            return _historyRepo.GetCard();
        }

        public DbSet<Books> getBooks()
        {
            return _historyRepo.GetBooks();
        }
    }
}
