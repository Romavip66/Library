using LibraryCourse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Abstractions
{
    public interface IHistoryR
    {
        void Add(History history);
        void Update(History history);
        void Delete(History history);
        Task Save();
        Task<List<History>> GetAll();
        Task<History> GetDetail(int? id);
        bool Exist(int id);
        DbSet<Library_Card> GetCard();
        DbSet<Books> GetBooks();
    }
}
