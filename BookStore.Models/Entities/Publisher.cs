using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Publisher : BaseEntity
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public string PublisherName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
