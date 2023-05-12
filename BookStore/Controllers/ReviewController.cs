using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;
        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest reviewReq)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await reviewService.AddReview(reviewReq, userId);
                return Ok(res);
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest("Phiên đăng nhập đã hết hạng!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewRequest reviewReq)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await reviewService.UpdateReview(reviewReq, userId);
                return Ok(res);
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest("Phiên đăng nhập đã hết hạng!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReview([FromBody] DeleteReviewRequest reviewReq)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await reviewService.DeleteReview(reviewReq, userId);
                return Ok(res);
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest("Phiên đăng nhập đã hết hạng!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
