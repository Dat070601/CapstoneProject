using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interfaces
{
    public interface ICartService
    {
        Task<CartResponse> AddCart(CartRequest req, Guid Id);
        Task<CartResponse> GetCart(Guid Id);
        Task<CartResponse> DeleteCart(List<CartOptionRequest> cartReq, Guid cusId);
        Task<CartResponse> UpdateCart(CartRequest cartReq, Guid cusId);
        Task<CartResponse> DeleteProductCart(CartRequest req, Guid id);
    }
}
