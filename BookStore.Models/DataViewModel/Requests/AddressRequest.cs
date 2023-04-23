using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class AddressRequest
    {
        public string? City { get; set; }
        public string? District { get; set; }
        public string? StreetAddress { get; set; }
    }
}
