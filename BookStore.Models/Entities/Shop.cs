using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Shop : BaseEntity
    {
        public Shop()
        {
            Images = new HashSet<Image>();
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int ShopWallet { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
