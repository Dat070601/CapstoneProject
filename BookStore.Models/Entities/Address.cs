using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class Address : BaseEntity
    {
        public Address()
        {

        }

        public string? Country { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public virtual Account Account { get; set; }
    }
}
