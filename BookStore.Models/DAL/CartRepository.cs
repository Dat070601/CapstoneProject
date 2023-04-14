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
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<Cart> GetCartByCustomerId(Guid id)
        {
            return await GetQuery(cart => cart.AccountId == id).SingleAsync();
        }
    }
}
