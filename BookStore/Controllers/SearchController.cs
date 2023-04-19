using BookStore.Service;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ISearcbService searcbService;
        public SearchController(ISearcbService searcbService)
        {
            this.searcbService = searcbService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchBook(string nameSearch)
        {
            var res = await searcbService.SearchBookWithFuzzy(nameSearch);
            return Ok(res);
        }
    }
}
