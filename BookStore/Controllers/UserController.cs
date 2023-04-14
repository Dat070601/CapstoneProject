using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.DTOs.Requests;
using BookStore.Service;
using BookStore.Service.Interfaces;
using BookStore.Service.TokenGenerators;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthenticateService authenticateService;
        private readonly RefreshTokenGenerator refreshTokenGenerator;
        public UserController(
            IUserService userService,
            IAuthenticateService authenticateService,
            RefreshTokenGenerator refreshTokenGenerator)
        {
            this.userService = userService;
            this.authenticateService = authenticateService;
            this.refreshTokenGenerator = refreshTokenGenerator;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var rs = await userService.Login(request);
            if(rs.IsSuccess)
            {
                var res = await authenticateService.Authenticate(rs.User!, "");
                if (res.IsSuccess)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res.Message);
                }
            }
            return BadRequest(rs.Message);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refreshRequest)
        {
            try
            {
                var rs = await refreshTokenGenerator.Refresh(refreshRequest.Token);
                if (rs.IsSuccess)
                {
                    var responseTokens = await authenticateService.Authenticate(rs.User!, "");
                    return Ok(responseTokens);
                }

                return BadRequest(rs.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var rs = await userService.Register(request);

            if (rs.IsSuccess)
            {
                return Ok("Vui lòng vào Email kiểm tra tin nhắn !");
            }

            return BadRequest(rs.Message);
        }

        [HttpGet("verify-account")]
        public async Task<IActionResult> VerifyAccount([FromQuery] string code)
        {
            var rs = await userService.CheckUserByActivationCode(new Guid(code));
            if (rs)
            {
                return Ok("Xác thực thành công !");
            }
            return BadRequest("Xác thực thất bại !");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var rs = await userService.ForgotPassword(request.Email!);
            if (rs.IsSuccess)
            {
                return Ok("Kiểm tra Email của bạn để thay đổi mật khẩu !");
            }
            return BadRequest(rs.Message);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var rs = await userService.ResetPassword(request);
            if (rs.IsSuccess)
            {
                return Ok("Bạn đã thay đổi mật khẩu thành công !");
            }
            return BadRequest(rs.Message);
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string code)
        {
            var rs = await userService.GetUserByResetCode(new Guid(code));
            if (rs)
            {
                // Redirect sang trang cập nhật mật khẩu, gửi kèm theo code
                return Redirect("https://localhost:7149/api/user/reset-password?code=" + code);
            }
            return BadRequest("Không tìm thấy tài khoản tương ứng !");
        }
    }
}
