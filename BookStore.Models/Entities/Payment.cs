using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class Payment : BaseEntity
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public string Type { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
