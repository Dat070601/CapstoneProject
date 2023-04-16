using BookStore.Models.DAL.Interfaces;
using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DAL
{
    public class BookPriceRepository : Repository<BookPrice>, IBookPriceRepository
    {
        public BookPriceRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<double> GetPriceByBookId(Guid id)
        {
            var price = await GetQuery(pr => pr.BookId == id && pr.IsActive == true).SingleAsync();
            if(price.ExpirationDate < DateTime.Now)
            {
                return price.BookSalePrice;
            }
            return price.BookDefaultPrice;
        }
    }
}
