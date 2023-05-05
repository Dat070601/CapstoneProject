using BookStore.Service;
using BookStore.Service.Interfaces;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearcbService searcbService;
        private readonly IBookService bookService;
        public SearchController(ISearcbService searcbService, IBookService bookService)
        {
            this.searcbService = searcbService;
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchBook(string nameSearch)
        {
            var res = await searcbService.SearchBookWithFuzzy(nameSearch);
            return Ok(res);
        }
    }
}
