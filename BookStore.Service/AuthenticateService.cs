using BookStore.Models.DAL;
using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using BookStore.Service.TokenGenerators;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class AuthenticateService : BaseService, IAuthenticateService
    {
        private readonly AccessTokenGenerator accessTokenGenerator;
        private readonly RefreshTokenGenerator refreshTokenGenerator;
        private readonly IRefreshTokenRepository refreshTokenRepo;
        private readonly IUserRepository userRepo;
        public AuthenticateService(
            IUnitOfWork unitOfWork,
            AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository) : base(unitOfWork)
        {
            this.accessTokenGenerator = accessTokenGenerator;
            this.refreshTokenGenerator = refreshTokenGenerator;
            this.userRepo = userRepository;
            this.refreshTokenRepo = refreshTokenRepository;
        }

        public async Task<TokenResponse> Authenticate(Account user, string listCredentials, string userGroup = "")
        {
            try
            {
                // Pre-handle user shop ID null
                Guid? userShopId = user.ShopId == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : user.ShopId;

                // 1. Generate access vs refresh token
                var accessToken = accessTokenGenerator.Generate(user, userShopId, listCredentials);
                var refreshToken = refreshTokenGenerator.Generate();

                // 2. Init refresh token properties
                var refreshTokenId = Guid.NewGuid();
                string refreshTokenHandler = new JwtSecurityTokenHandler().WriteToken(refreshToken);

                // 3. Create user refresh token
                RefreshToken userRefreshToken = new()
                {
                    Id = refreshTokenId,
                    AccountId = user.Id,
                    Token = refreshTokenHandler,
                };

                await refreshTokenRepo.AddAsync(userRefreshToken);
                await unitOfWork.CommitTransaction();


                return new TokenResponse()
                {
                    IsSuccess = true,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    RefreshToken = refreshTokenHandler,
                    ShopId = (Guid)userShopId,
                    //Wallet = user.Wallet.HasValue == false ? 0 : user.Wallet
                };
            }
            catch (Exception e)
            {
                await unitOfWork.RollbackTransaction();
                return new TokenResponse()
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message,
                };
            }
        }
    }
}
