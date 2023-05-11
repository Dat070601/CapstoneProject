using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart([FromBody] CartRequest cartRequest)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await cartService.AddCart(cartRequest, userId);
                return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
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

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await cartService.GetCart(userId);
                return Ok(res);
            }
            catch(IndexOutOfRangeException)
            {
                return BadRequest("Phiên đăng nhập đã hết hạng!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBookInCart([FromBody] List<CartOptionRequest> cartReq)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await cartService.DeleteCart(cartReq, userId);
                if(res.IsSuccess)
                {
                    return Ok(res.Message);
                }
                return BadRequest(res.Message);
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
        public async Task<IActionResult> UpdateBookInCart([FromBody] CartRequest cartReq)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await cartService.UpdateCart(cartReq, userId);
                if(res.IsSuccess)
                {
                    return Ok(res.Message);
                }
                return BadRequest(res.Message);
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
