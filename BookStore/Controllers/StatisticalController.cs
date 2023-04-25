using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService statisticalService;
        public StatisticalController(
            IStatisticalService statisticalService)
        {
            this.statisticalService = statisticalService;    
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistical(string type)
        {
            var res = await statisticalService.NumberOfBooksSold(type);
            return Ok(res);
        }

        [HttpGet("statistical-in-month")]
        public async Task<IActionResult> GetStatisticalInMonth(int month)
        {
            var res = await statisticalService.NumberOfBookSoldInMonth(month, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            if (res.Data.Count == 0)
            {
                return Ok("Không có dữ liệu vào thời gian này !");
            }
            return Ok(res);
        }
    }
}
