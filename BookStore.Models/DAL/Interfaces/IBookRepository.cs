using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetAllProductPaging(int page, int pageSize);
        Task<List<Book>> GetTopNewBook();
        Task<List<Book>> GetBestSeller();
        Task<List<Book>> SearchBookWithFuzzy(string nameBook);
        Task<decimal> NumberOfPages();
        Task<List<Book>> GetFourBook(Guid bookId);
        Task<int> BookCount();
    }
}
