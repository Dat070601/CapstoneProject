using BookStore.Models.DTOs.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DataViewModel.Responses
{
    public class PaymentLinkResponse : GeneralResponses
    {
        public string? RedirectUrl { get; set; }
    }
}
