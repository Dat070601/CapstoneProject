using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.DTOs.Responses;
using BookStore.Service.TokenValidators;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookStore.Service.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly IRefreshTokenRepository refreshTokenRepo;
        private readonly TokenGenerator tokenGenerator;
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepo;
        private readonly RefreshTokenValidator refreshTokenValidator;
        public RefreshTokenGenerator(
                        IRefreshTokenRepository refreshTokenRepo, 
                        TokenGenerator tokenGenerator, 
                        IConfiguration configuration, 
                        IUnitOfWork unitOfWork, 
                        RefreshTokenValidator refreshTokenValidator, 
                        IUserRepository userRepo)
        {
            this.unitOfWork = unitOfWork;
            this.refreshTokenRepo = refreshTokenRepo;
            this.tokenGenerator = tokenGenerator;
            this.configuration = configuration;
            this.userRepo = userRepo;
            this.refreshTokenValidator = refreshTokenValidator;
        }
        public async Task<UserResponse> Refresh(string tokenContent)
        {
            // 1. Check if refresh token is valid
            var validRefreshToken = refreshTokenValidator.Validate(tokenContent);

            if (!validRefreshToken.IsSuccess)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = validRefreshToken.Message
                };
            }

            // 2. Get refresh token by token
            var rs = await GetByToken(tokenContent);

            if (!rs.IsSuccess)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = rs.Message
                };
            }

            var refreshTokenDTO = rs.RefreshToken;

            // 3. Delete that refresh token
            var deleteRefreshToken = await Delete(refreshTokenDTO.Id);
            if (!deleteRefreshToken.IsSuccess)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = deleteRefreshToken.Message
                };
            }
            // 4. Find user have that refresh token
            var user = refreshTokenDTO.Account;
            await unitOfWork.CommitTransaction();

            return new UserResponse
            {
                IsSuccess = true,
                User = user
            };
        }

        public JwtSecurityToken Generate()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:RefreshTokenSecret"]));
            var issuer = configuration["AuthSettings:Issuer"];
            var audience = configuration["AuthSettings:Audience"];
            var expires = DateTime.Now.AddMonths(4); // expires in 4 months later

            return tokenGenerator.GenerateToken(key, issuer, audience, expires);
        }
        private async Task<RefreshTokenResponse> GetByToken(string token)
        {
            var refreshToken = await refreshTokenRepo.FindAsync(tk => tk.Token == token);

            if (refreshToken == null)
            {
                return new RefreshTokenResponse
                {
                    IsSuccess = false,
                    Message = "Refresh Token không tìm thấy trong cơ sở dữ liệu !",
                };
            }

            return new RefreshTokenResponse
            {
                IsSuccess = true,
                RefreshToken = refreshToken
            };
        }
        private async Task<RefreshTokenResponse> Delete(Guid tokenId)
        {
            try
            {
                await refreshTokenRepo.Delete(tk => tk.Id == tokenId);
                return new RefreshTokenResponse
                {
                    IsSuccess = true,
                };
            }
            catch (Exception e)
            {
                return new RefreshTokenResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }
    }
}
