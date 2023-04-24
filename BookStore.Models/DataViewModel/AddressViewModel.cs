using BookStore.Models.DTOs.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel
{
    public class AddressViewModel
    {
        public string? City { get; set; }
        public string? District { get; set; }
        public string? StreetAddress { get; set; }
    }
}
