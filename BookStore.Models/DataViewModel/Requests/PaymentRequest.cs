using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class PaymentRequest
    {
        public Guid OrderId { get; set; }
        public string? OrderType { get; set; }
    }
}
