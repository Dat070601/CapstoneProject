using BookStore.Models.DAL.Interfaces;
using BookStore.Models.Entities;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.DAL
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DbFactory dbFactory) : base(dbFactory)
        {

        }
        public async Task<List<Book>> GetAllProductPaging(int page, int pageSize)
        {
            var req = await GetAllPaging().OrderBy(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return req;
        }

        public async Task<List<Book>> GetTopNewBook()
        {
            return await GetAllPaging().OrderByDescending(or => or.DateCreated).Take(5).ToListAsync();
        }

        public async Task<List<Book>> GetBestSeller()
        {
            return await GetAllPaging().OrderByDescending(or => or.Sold).Take(5).ToListAsync();
        }
        public async Task<List<Book>> SearchBookWithFuzzy(string nameBook)
        {
            var books = await GetAll();
            var listBooks = new List<Book>();
            foreach (var book in books )
            {
                var ratioWeighted = Fuzz.WeightedRatio(nameBook, book.BookName);
                if(ratioWeighted > 60)
                {
                    listBooks.Add(book);
                };
            }
            return listBooks;
        }

        public async Task<decimal> NumberOfPages()
        {
            var page = await GetAll();
            var numPage = (decimal)page.Count / 20;
            return Math.Ceiling(numPage);
        }

        public async Task<List<Book>> GetFourBook(Guid bookId)
        {
            var book = await GetQuery(b => b.Id == bookId).SingleAsync();
            return await GetQuery(c => c.CategoryId == book.CategoryId && c.Id != bookId).Take(4).ToListAsync();
        }
    }
}
