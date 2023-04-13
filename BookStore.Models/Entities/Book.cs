using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Book : BaseEntity
    {
        public Book()
        {
            Images = new HashSet<Image>();
            Reviews = new HashSet<Review>();
            CartDetails = new HashSet<CartDetail>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid CategoryId { get; set; }
        public string? BookName { get; set; }
        public string? BookDescription { get; set; }
        public bool IsActive { get; set; }
        public int Sold { get; set; }
        public DateTime DateCreated { get; set; }
        public int Quantity { get; set; }
        public int NumPage { get; set; }
        public Guid? PublisherId { get; set; }
        public Guid? AuthorId { get; set; }
        public virtual BookPrice BookPrice { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
