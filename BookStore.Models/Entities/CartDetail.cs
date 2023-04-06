using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class CartDetail : BaseEntity
    {
        public CartDetail()
        {

        }

        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public Guid CartId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
