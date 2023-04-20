using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;
        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> AllAuthor()
        {
            var res = await authorService.GetListAuthor();
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookByAuthorId(Guid id)
        {
            var res = await authorService.GetBookByAuthorId(id);
            return Ok(res);
        }
    }
}
