using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService addressService;
        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressRequest addressRequest)
        {
            var res = await addressService.AddAddress(addressRequest, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddress()
        {
            var res = await addressService.GetAddress(new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAddress(Guid addressId)
        {
            var res = await addressService.DeleteAddress(addressId, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddressById([FromBody] AddressRequest addressRequest, Guid addressId)
        {
            var res = await addressService.UpdateAddress(addressRequest,addressId, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return res.IsSuccess ? Ok(res.Message) : BadRequest(res.Message);
        }
    }
}
