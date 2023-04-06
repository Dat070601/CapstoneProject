using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class BookPrice : BaseEntity
    {
        public BookPrice()
        {

        }

        public Guid BookId { get; set; }
        public double BookDefaultPrice { get; set; }
        public double BookSalePrice { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual Book Book { get; set; }
    }
}
