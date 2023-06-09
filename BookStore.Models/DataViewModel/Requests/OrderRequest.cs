﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class OrderRequest
    {
        public Guid PaymentId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address{ get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Message { get; set; }
        public List<OrderDetailRequest>? OrderDetails { get; set; }
    }
}
