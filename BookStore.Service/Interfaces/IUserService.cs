using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.DTOs.Requests;
using BookStore.Models.DTOs.Responses;

namespace BookStore.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> Login(LoginRequest req);
        Task<UserResponse> Register(RegisterRequest req);
        Task<bool> CheckUserByActivationCode(Guid activationCode);
        Task<UserResponse> ForgotPassword(string userEmail);
        Task<bool> GetUserByResetCode(Guid resetPassCode);
        Task<UserResponse> ResetPassword(ResetPasswordRequest req);
        Task<UserProfileResponse> GetUserProfile(Guid userId);
    }
}
