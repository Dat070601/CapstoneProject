using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interfaces
{
    public interface IAuthenticateService
    {
        Task<TokenResponse> Authenticate(Account user, string listCredentials, string userGroup = "");
    }
}
