using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> ViewBook(int page, int pageSize = 20)
        {
            var res = await bookService.GetBooksPaging(page, pageSize);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ViewDetailBook(Guid id)
        {
            var res = await bookService.GetBookById(id);
            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookRequest bookRequest)
        {
            var res = await bookService.AddBook(bookRequest);
            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        //[Authorize]
        [HttpPut("stop-production-book")]
        public async Task<IActionResult> StopProductionBook(Guid bookId)
        {
            var res = await bookService.StopProductionBook(bookId);
            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }

        [HttpGet("product-top-new")]
        public async Task<IActionResult> GetProductTopNew()
        {
            var res = await bookService.GetBookTopNew();
            return Ok(res);
        }

        [HttpGet("product-most-seller")]
        public async Task<IActionResult> GetProductMostSeller()
        {
            var res = await bookService.GetBookBestSeller();
            return Ok(res);
        }

        [HttpGet("number-page")]
        public async Task<IActionResult> NumbarOfPage()
        {
            return Ok(await bookService.NumberOfPages());
        }

        [HttpGet("book-same-cate/{bookId}")]
        public async Task<IActionResult> BookSameCate(Guid bookId)
        {
            return Ok(await bookService.GetFourBook(bookId));
        }
    }
}
