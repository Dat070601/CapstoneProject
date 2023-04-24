using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Responses
{
    public class DateResponse
    {
        public string? Date { get; set; }
        public List<StatisBookResponse>? StatisBooks { get; set; }
    }
}
