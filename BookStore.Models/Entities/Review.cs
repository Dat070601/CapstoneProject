using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class Review : BaseEntity
    {
        public Review()
        {

        }

        public Guid AccountId { get; set; }
        public Guid ProductId { get; set; }
        public string? ReviewText { get; set; }
        public virtual Book Book { get; set; }
        public virtual Account Account { get; set; }
    }
}
