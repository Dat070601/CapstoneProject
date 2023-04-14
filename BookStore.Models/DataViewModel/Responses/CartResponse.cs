using BookStore.Models.DTOs.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Responses
{
    public class CartResponse : GeneralResponses
    {
        public List<CartDetailViewModel>? cartDetailViewModels { get; set; } = new List<CartDetailViewModel>(); 
    }
}
