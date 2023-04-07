using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class UserGroup : BaseEntity
    {
        public UserGroup()
        {
            Accounts = new HashSet<Account>();
            Credentials = new HashSet<Credential>();
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Credential> Credentials { get; set; }
    }
}
