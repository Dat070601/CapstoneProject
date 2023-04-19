using BookStore.Models.DAL.Interfaces;
using BookStore.Models.Entities;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
