using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;
        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        // GET: api/<BookController>
        [HttpGet]
        public async Task<IActionResult> ViewBook(int page, int pageSize = 10)
        {
            var res = await bookService.GetBooksPaging(page, pageSize);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ViewDetailBook(Guid id)
        {
            var res = await bookService.GetBookById(id);
            return res.IsSuccess ? Ok(res) : BadRequest(res.ErrorMessage);
        }
    }
}
