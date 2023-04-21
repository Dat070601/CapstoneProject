using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class BookPriceRequest
    {
        public double BookDefautPrice { get; set; }
        public double BookSalePrice { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
