using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Author : BaseEntity
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public string? AuthorName { get; set; }
        public virtual ICollection<Book> Books {get; set;}
    }
}
