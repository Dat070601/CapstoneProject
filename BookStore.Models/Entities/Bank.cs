using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class Bank : BaseEntity
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
        public BankType BankType { get; set; }
        public Account Account { get; set; }
    }
}
