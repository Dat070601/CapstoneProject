using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class Status : BaseEntity
    {
        public Status()
        {
            Orders = new HashSet<Order>();
            HistoryTransactions = new HashSet<HistoryTransaction>();
        }

        public string NameStatus { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<HistoryTransaction> HistoryTransactions { get; set; }
    }
}
