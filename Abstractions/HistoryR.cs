using LibraryCourse.Data;
using LibraryCourse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Abstractions
{
    public class HistoryR
    {
        readonly LibraryContext _context;
        public HistoryR(LibraryContext context)
        {
            _context = context;
        }

        public void Add(History history)
        {
            _context.Add(history);
        }

        public void Update(History history)
        {
            _context.Update(history);
        }

        public void Delete(History history)
        {
            _context.Remove(history);
        }

        public bool Exist(int id)
        {
            return _context.History.Any(m => m.Id == id);
        }

        public Task<List<History>> GetAll()
        {
            return _context.History.ToListAsync();
        }

        public Task<History> GetDetail(int? id)
        {
            return _context.History.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        public DbSet<Library_Card> GetCard()
        {
            return _context.Library_Card;
        }

        public DbSet<Books> GetBooks()
        {
            return _context.Books;
        }
    }
}
