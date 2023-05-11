using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await orderService.AddOrder(orderRequest, userId);
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

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var res = await orderService.GetOrder(orderId);
            return Ok(res);
        }

        [HttpPut("status/{orderId}")]
        public async Task<IActionResult> UpdateStatus([FromBody] StatusRequest status, Guid orderId)
        {
            var res = await orderService.ChangeStatus(status, orderId);
            return Ok(res);
        }

        [HttpGet("order-from-customer")]
        public async Task<IActionResult> GetOrderByCus()
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await orderService.GetOrderByCusId(userId);
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
