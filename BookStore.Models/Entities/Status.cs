using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Status : BaseEntity
    {
        public Status()
        {

        }

        public string NameStatus { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<HistoryTransaction> HistoryTransactions { get; set; }
    }
}
