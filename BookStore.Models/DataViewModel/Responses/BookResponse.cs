using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Responses
{
    public class BookResponse
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; }
    }
}
