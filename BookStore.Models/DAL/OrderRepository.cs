using BookStore.Models.DAL.Interfaces;
using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.DAL
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<List<Order>> GetOrderByDate(string dateTime)
        {
            if (dateTime.Length == 10)
            {
                return await GetQuery(or => or.DateCreated.ToString().Substring(0, 10).Equals(dateTime) && or.Status.NameStatus.Equals("Đã Giao")).ToListAsync();
            }
            if (dateTime.Length == 13)
            {
                return await GetQuery(or => or.DateCreated.ToString().Substring(0, 13).Equals(dateTime) && or.Status.NameStatus.Equals("Đã Giao")).ToListAsync();
            }
            return null!;
        }
    }
}
