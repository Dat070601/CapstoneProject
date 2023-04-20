using BookStore.Models.DAL.Interfaces;
using BookStore.Models.Entities;

namespace BookStore.Models.DAL
{
    public class HistoryTransactionsRepository : Repository<HistoryTransaction>, IHistoryTransactionsRepository
    {
        public HistoryTransactionsRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
