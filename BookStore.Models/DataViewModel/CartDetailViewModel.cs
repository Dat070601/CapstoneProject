using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel
{
    public class CartDetailViewModel
    {
        public Guid BookId { get; set; }
        public string? BookName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public double TotalPrice { get; set; }

    }
}
