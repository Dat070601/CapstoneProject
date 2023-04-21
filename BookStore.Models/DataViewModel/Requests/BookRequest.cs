using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class BookRequest 
    {
        public string? BookName { get; set; }
        public string? BookDescription { get; set; }
        public Guid CategoryId { get; set; }
        public int Quantity { get; set; }
        public int NumPage { get; set; }
        public Guid PublisherId { get; set; }
        public Guid AuthorId { get; set; }
        public List<string>? ImageUrls { get; set; }
        public BookPriceRequest? Price { get; set; }
    }
}
