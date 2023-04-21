using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class CategoryRequest
    {
        public Guid SubId { get; set; }
        public string? CategoryName { get; set; }
    }
}
