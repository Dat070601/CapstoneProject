using BookStore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Entities
{
    public partial class HistoryTransaction : BaseEntity
    {
        public HistoryTransaction()
        {

        }

        public Guid BillId { get; set; }
        public Guid AccountId { get; set; }
        public double Money { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public virtual Status Status { get; set; }
    }
}
