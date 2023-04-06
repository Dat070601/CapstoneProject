using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class RefreshToken : BaseEntity
    {
        public RefreshToken()
        {

        }
        public string Token { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
