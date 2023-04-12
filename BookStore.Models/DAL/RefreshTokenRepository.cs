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
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task DeleteAll(Guid userId)
        {
            var listTokens = await GetQuery(u => u.AccountId.Equals(userId)).ToListAsync();
            Delete(listTokens);
        }
    }
}
