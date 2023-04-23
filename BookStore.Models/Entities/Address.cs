using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Address : BaseEntity
    {
        public Address()
        {

        }

        public string? District { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
