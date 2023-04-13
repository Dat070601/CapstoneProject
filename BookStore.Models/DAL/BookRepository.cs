using BookStore.Models.DAL.Interfaces;
using BookStore.Models.Entities;
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
    }
}
