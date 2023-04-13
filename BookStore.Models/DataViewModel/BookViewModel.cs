using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public double DefaultPrice { get; set; }
        public double SalePrice { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int Sold { get; set; }
        public string? ImageUrl { get; set; }
    }
}
