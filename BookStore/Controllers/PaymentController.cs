using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var url = await paymentService.CreatePayemntUrl(paymentRequest,HttpContext);
            return Ok(url);
        }

        [HttpGet("callback-payment")]
        public async Task<IActionResult> CallBackPayment()
        {
            var call = await paymentService.PaymentExecute(Request.Query, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return call.IsSuccess ? Ok(call) : BadRequest(call.Message);
        }
    }
}
