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

        public async Task<List<Order>> GetOrderByDate(DateTime dateTime)
        {
            var listOrder = await GetQuery(or => or.DateCreated.Date == dateTime.Date && or.StatusId.ToString().Equals("10C70526-DEB7-44C2-A0F8-74C7A5CD4092")).ToListAsync();
            return listOrder;
        }

        public async Task<List<Order>> GetOrderInMonth(int month)
        {
            var listOrder = await GetQuery(or => or.DateCreated.Month == month && or.StatusId.ToString().Equals("10C70526-DEB7-44C2-A0F8-74C7A5CD4092")).ToListAsync();
            return listOrder;
        }
    }
}
