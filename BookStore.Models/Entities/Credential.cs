using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class Credential : BaseEntity
    {
        public Credential()
        {

        }

        public Guid UserGroupId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActivated { get; set; }
        public UserGroup UserGroup { get; set; }
        public Role Role { get; set; }
    }
}
