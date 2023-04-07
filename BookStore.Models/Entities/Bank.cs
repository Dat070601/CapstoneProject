using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Bank : BaseEntity
    {
        public Bank()
        {

        }

        public string? BankNumber { get; set; }
        public string? AccountName { get; set; }
        public Guid AccountId { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public Guid BankTypeId { get; set; }
        public virtual BankType BankType { get; set; }
        public virtual Account Account { get; set; }
    }
}
