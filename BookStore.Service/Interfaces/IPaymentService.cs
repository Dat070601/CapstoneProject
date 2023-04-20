using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePayemntUrl(PaymentRequest paymentRequest, HttpContext context);
        Task<PaymentResponse> PaymentExecute(IQueryCollection collections, Guid accountId);
    }
}
