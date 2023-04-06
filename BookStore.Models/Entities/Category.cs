using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Categories = new HashSet<Category>();
        }

        public string? CategoryName { get; set; }
        public Guid SubId { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}
