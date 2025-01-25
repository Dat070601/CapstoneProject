using BookStore.Models.DataViewModel;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;

namespace BookStore.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> AddOrder(OrderRequest orderRequest, Guid cusId);
        Task<OrderViewModel> GetOrder(Guid orderId);
        Task<OrderResponse> ChangeStatus(StatusRequest status, Guid orderId);
        Task<OrderViewModel> GetOrderByCusId(Guid cusId);
        Task<List<OrderViewModel>> GetAllOrder();
    }
} 