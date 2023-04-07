using BookStore.Models.Base;

namespace BookStore.Models.Entities
{
    public partial class BankType : BaseEntity
    {
        public BankType()
        {
            Banks = new HashSet<Bank>();
        }

        public string BankName { get; set; }
        public string BankCode { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
    }
}
