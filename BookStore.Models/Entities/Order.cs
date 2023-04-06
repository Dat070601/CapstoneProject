using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }

        public Guid PaymentId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime DateCreated { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public double Total { get; set; }
        public Guid BillId { get; set; }
        public Guid StatusId { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Status> Statuses { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
