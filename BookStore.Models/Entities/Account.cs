using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class Account : BaseEntity
    {
        public Account()
        {
            Addresses = new HashSet<Address>();
            Reviews = new HashSet<Review>();
            Banks = new HashSet<Bank>();
            Orders = new HashSet<Order>();
        }

        public Guid ShopId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public int Wallet { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserGroupId { get; set; }
        public bool IsActived { get; set; }
        public Guid ActivationCode { get; set; }
        public Guid ResetPasswordCode { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<Address> Addresses  { get; set; }
        public virtual ICollection<Review> Reviews  { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
