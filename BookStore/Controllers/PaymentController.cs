﻿using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] PaymentRequest paymentRequest)
        {
            var res = await paymentService.CreatePayemntUrl(paymentRequest,HttpContext);
            return Ok(res);
        }

        [HttpGet("callback-payment")]
        public async Task<IActionResult> CallBackPayment()
        {
            var call = await paymentService.PaymentExecute(Request.Query);
            return call.IsSuccess ? Redirect("http://localhost:5173/payment-success") : BadRequest(call.Message);
        }
    }
}
