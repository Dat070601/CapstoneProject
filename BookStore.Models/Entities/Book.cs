using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class Book : BaseEntity
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
            BookPrices = new HashSet<BookPrice>();
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            CartDetails = new HashSet<CartDetail>();
        }

        public Guid CategoryId { get; set; }
        public string? BookName { get; set; }
        public string? BookDescription { get; set; }
        public bool IsActive { get; set; }
        public int Sold { get; set; }
        public DateTime DateCreated { get; set; }
        public int Quantity { get; set; }
        public int NumPage { get; set; }
        public int CountView { get; set; }
        public Guid PublisherId { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookPrice> BookPrices { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
