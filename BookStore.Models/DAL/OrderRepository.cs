using BookStore.Models.DAL.Interfaces;
using BookStore.Models.Entities;

namespace BookStore.Models.DAL
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
