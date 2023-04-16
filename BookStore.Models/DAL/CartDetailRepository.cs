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
    public class CartDetailRepository : Repository<CartDetail>, ICartDetailRepository
    {
        public CartDetailRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<CartDetail> GetCartDetailByCartIdAndBookId(Guid cartId, Guid bookId)
        {
            var req = await GetQuery(o => o.CartId == cartId && o.BookId == bookId).AnyAsync();
            return req ? await GetQuery(o => o.CartId == cartId && o.BookId == bookId).SingleAsync() : null!;
        }

        public async Task<List<CartDetail>> GetListCartDetailByCartId(Guid cartId)
        {
            return await GetQuery(cd => cd.CartId == cartId).ToListAsync();
        }
    }
}
