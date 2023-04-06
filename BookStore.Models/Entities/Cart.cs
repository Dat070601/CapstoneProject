namespace BookStore.Models.Entities
{
    public class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }
        public Guid AccountId { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
