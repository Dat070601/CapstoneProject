using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Responses
{
    public class StatisResponse
    {
        public string? Color { get; set; }
        public string? Unit { get; set; }
        public string? Title { get; set; }
        public List<DateResponse>? DateResponse { get; set; }
    }
}
