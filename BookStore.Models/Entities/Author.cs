using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Author : BaseEntity
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public string? AuthorName { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors {get; set;}
    }
}
