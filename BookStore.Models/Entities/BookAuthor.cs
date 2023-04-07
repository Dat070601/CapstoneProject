using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class BookAuthor : BaseEntity
    {
        public BookAuthor()
        {

        }

        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
