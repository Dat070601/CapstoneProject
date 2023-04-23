using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService addressService;
        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressRequest addressRequest)
        {
            var res = await addressService.AddAddress(addressRequest, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddress()
        {
            var res = await addressService.GetAdderess(new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAddress(Guid addressId)
        {
            var res = await addressService.DeleteAddress(addressId, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }
    }
}
