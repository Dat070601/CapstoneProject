using BookStore.Models.DTOs.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel
{
    public class OrderViewModel : GeneralResponses
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string? MessageOrder { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public List<OrderDetailViewModel>? OrderDetails { get; set; }
    }
}
