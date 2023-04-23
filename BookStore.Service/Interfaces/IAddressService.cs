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
    public interface IAddressService
    {
        Task<AddressResponse> AddAddress(AddressRequest addReq, Guid acountId);
        Task<List<string>> GetAdderess(Guid accountId);
        Task<AddressResponse> DeleteAddress(Guid addressId, Guid accountId);
    }
}
