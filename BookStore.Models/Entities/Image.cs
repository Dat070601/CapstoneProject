using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Image : BaseEntity
    {
        public Image()
        {

        }

        public string? ImageUrl { get; set; }
        public Guid? BookId { get; set; }
        public Guid? ShopId { get; set; }
        public Guid? AccountId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual Account Account { get; set; }
    }
}
