using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Requests
{
    public class CartRequest
    {
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
