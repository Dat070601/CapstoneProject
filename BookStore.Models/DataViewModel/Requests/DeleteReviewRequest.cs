using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class DeleteReviewRequest
    {
        public Guid ReviewId { get; set; }
        public Guid AccountId { get; set; }
    }
}
