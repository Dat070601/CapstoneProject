using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Cart : BaseEntity
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }
        public Guid AccountId { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
