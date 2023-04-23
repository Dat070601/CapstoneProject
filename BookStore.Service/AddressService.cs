using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly IAddressRepository addressRepository;
        public AddressService(
            IUnitOfWork unitOfWork, 
            IMapperCustom mapperCustom,
            IAddressRepository addressRepository) : base(unitOfWork, mapperCustom)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<AddressResponse> AddAddress(AddressRequest addReq, Guid accountId)
        {
            var address = new Address
            {
                AccountId = accountId,
                City = addReq.City,
                District = addReq.District,
                StreetAddress = addReq.StreetAddress
            };
            await addressRepository.AddAsync(address);
            await unitOfWork.CommitTransaction();
            return new AddressResponse
            {
                IsSuccess = true,
                Message = "Add address success!"
            };
        }

        public async Task<AddressResponse> DeleteAddress(Guid addressId, Guid accountId)
        {
            var address = await addressRepository.GetQuery(ad => ad.Id == addressId && ad.AccountId == accountId).SingleAsync();
            if (address == null)
            {
                return new AddressResponse
                {
                    IsSuccess = false,
                    Message = "Address not found!"
                };
            }
            addressRepository.Delete(address);
            await unitOfWork.CommitTransaction();
            return new AddressResponse
            {
                IsSuccess = true,
                Message = "Delete address succress!"
            };
        }

        public async Task<List<string>> GetAdderess(Guid accountId)
        {
            var listAddress = await addressRepository.GetQuery(ad => ad.AccountId == accountId).ToListAsync();
            var addresses = new List<string>();
            foreach (var item in listAddress)
            {
                var address = item.StreetAddress + ", quận " + item.District + ", thành phố " + item.City;
                addresses.Add(address);
            }
            return addresses;
        }
    }
}
