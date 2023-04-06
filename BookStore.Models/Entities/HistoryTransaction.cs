using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public class HistoryTransaction : BaseEntity
    {
        public HistoryTransaction()
        {

        }

        public Guid BillId { get; set; }
        public Guid AccountId { get; set; }
        public double Money { get; set; }
        public DateTime TransactionDate { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
