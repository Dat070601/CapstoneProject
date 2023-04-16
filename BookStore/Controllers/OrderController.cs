using BookStore.Models.DataViewModel.Requests;
using BookStore.Service;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            //var handler = new JwtSecurityTokenHandler();
            //var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            //var tokenString = handler.ReadToken(token) as JwtSecurityToken;
            //var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var res = await orderService.AddOrder(orderRequest, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return res.IsSuccess ? Ok(res.Message) : BadRequest(res.Message);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var res = await orderService.GetOrder(orderId);
            return Ok(res);
        }
    }
}
