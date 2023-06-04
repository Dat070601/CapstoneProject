using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

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
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await bookService.AddBook(bookRequest);
                return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest("Phiên đăng nhập đã hết hạng!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpGet("book-count")]
        public async Task<IActionResult> CountBook()
        {
            return Ok(await bookService.GetCountBook());
        }
    }
}
