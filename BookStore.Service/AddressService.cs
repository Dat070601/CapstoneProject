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

        public async Task<List<AddressViewModel>> GetAddress(Guid accountId)
        {
            var listAddress = await addressRepository.GetQuery(ad => ad.AccountId == accountId).ToListAsync();
            var addresses = new List<AddressViewModel>();
            foreach (var item in listAddress)
            {
                var address = new AddressViewModel
                {
                    StreetAddress = item.StreetAddress,
                    District = item.District,
                    City = item.City
                };
                addresses.Add(address);
            }
            return addresses;
        }

        public async Task<AddressResponse> UpdateAddress(AddressRequest addressReq, Guid addressId, Guid accountId)
        {
            var address = await addressRepository.GetQuery(ad => ad.Id == addressId && ad.AccountId == accountId).SingleAsync();
            if (address == null)
            {
                return new AddressResponse
                {
                    IsSuccess = false,
                    Message = "Can't find Address!"
                };
            }
            address.StreetAddress = addressReq.StreetAddress;
            address.City = addressReq.City;
            address.District = addressReq.District;
            addressRepository.Update(address);
            return new AddressResponse
            {
                IsSuccess = true,
                Message = "Update address success!"
            };
        }
    }
}
