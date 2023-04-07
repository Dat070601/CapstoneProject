using BookStore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Entities
{
    public partial class RefreshToken : BaseEntity
    {
        public RefreshToken()
        {

        }
        public string Token { get; set; }
        public Guid AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public virtual Account Account { get; set; }
    }
}
