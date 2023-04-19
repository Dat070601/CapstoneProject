using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        public Guid SubId { get; set; }
        public string? NameCategory { get; set; }
        public List<BookViewModel>? Books { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
    }
}
