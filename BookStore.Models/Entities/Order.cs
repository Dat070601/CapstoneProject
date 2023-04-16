using BookStore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Entities
{
    public partial class Order : BaseEntity
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
        public string? Message { get; set; }
        public Guid BillId { get; set; }
        public Guid StatusId { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Account Account { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
