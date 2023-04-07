﻿using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            Credentials = new HashSet<Credential>();
        }

        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Credential> Credentials { get; set; }
    }
}
