using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class OrderDetail : BaseEntity
    {
        public OrderDetail()
        {

        }

        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }
    }
}
