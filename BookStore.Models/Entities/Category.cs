using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public string? CategoryName { get; set; }
        public Guid? SubId { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}
