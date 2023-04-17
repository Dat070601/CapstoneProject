using BookStore.Models.DataViewModel;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> AddOrder(OrderRequest orderRequest, Guid cusId);
        Task<OrderViewModel> GetOrder(Guid orderId);
        Task<OrderResponse> ChangeStatus(StatusRequest status, Guid orderId);
    }
}
