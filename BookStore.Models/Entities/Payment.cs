using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Payment : BaseEntity
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public string Type { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
