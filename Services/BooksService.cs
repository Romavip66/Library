using LibraryCourse.Abstractions;
using LibraryCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Services
{
    public class BooksService
    {
        private readonly IBooksR _booksRepo;
        public BooksService(IBooksR booksRepo)
        {
            _booksRepo = booksRepo;
        }

        public async Task<List<Books>> GetBooks()
        {
            return await _booksRepo.GetAll();
        }

        public async Task<Books> DetailsBooks(int? id)
        {
            return await _booksRepo.GetDetail(id);
        }
        public bool BooksExis(int id)
        {
            return _booksRepo.Exist(id);
        }
        public async Task AddAndSave(Books books)
        {
            _booksRepo.Add(books);
            await _booksRepo.Save();
        }

        public async Task Update(Books books)
        {
            _booksRepo.Update(books);
            await _booksRepo.Save();
        }

        public async Task Delete(Books books)
        {
            _booksRepo.Delete(books);
            await _booksRepo.Save();
        }
    }
}
