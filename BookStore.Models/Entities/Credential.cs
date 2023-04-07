using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Credential : BaseEntity
    {
        public Credential()
        {

        }

        public Guid UserGroupId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActivated { get; set; }
        public virtual UserGroup UserGroup { get; set; }
        public virtual Role Role { get; set; }
    }
}
