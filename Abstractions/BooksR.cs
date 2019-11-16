using LibraryCourse.Data;
using LibraryCourse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Abstractions
{
    public class BooksR
    {
        readonly LibraryContext _context;
        public BooksR(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Books books)
        {
            _context.Add(books);
        }

        public void Update(Books books)
        {
            _context.Update(books);
        }

        public void Delete(Books books)
        {
            _context.Remove(books);
        }

        public bool Exist(int id)
        {
            return _context.Books.Any(m => m.Id == id);
        }

        public Task<List<Books>> GetAll()
        {
            return _context.Books.ToListAsync();
        }

        public Task<Books> GetDetail(int? id)
        {
            return _context.Books.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}
