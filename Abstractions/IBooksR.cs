using LibraryCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Abstractions
{
    public interface IBooksR
    {
        void Add(Books books);
        void Update(Books books);
        void Delete(Books books);
        Task Save();
        Task<List<Books>> GetAll();
        Task<Books> GetDetail(int? id);
        bool Exist(int id);
    }
}
