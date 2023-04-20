﻿using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var handler = new JwtSecurityTokenHandler();
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var tokenString = handler.ReadToken(token) as JwtSecurityToken;
            var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            var res = await cartService.AddCart(cartRequest, userId);
            return res.IsSuccess ? Ok(res.Message) : BadRequest(res.Message);
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetProduct()
        {
            //var handler = new JwtSecurityTokenHandler();
            //var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            //var tokenString = handler.ReadToken(token) as JwtSecurityToken;
            //var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            //var res = await cartService.GetCart(userId);
            var res = await cartService.GetCart(new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBookInCart([FromBody] List<CartOptionRequest> cartReq)
        {
            var res = await cartService.DeleteCart(cartReq, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            if(res.IsSuccess)
            {
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookInCart([FromBody] CartRequest cartReq)
        {
            var res = await cartService.UpdateCart(cartReq, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            if(res.IsSuccess)
            {
                return Ok(res.Message);
            }
            return BadRequest(res.Message);
        }
    }
}