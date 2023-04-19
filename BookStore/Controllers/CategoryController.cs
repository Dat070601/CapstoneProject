using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Service;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("sub")]
        public async Task<IActionResult> GetCategory()
        {
            var res = await categoryService.GetSubCategory();
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookByCategory(Guid cateId, int page, int pageSize = 10)
        {
            var res = await categoryService.GetBookByCategoryId(cateId, page, pageSize);
            return Ok(res);
        }
    }
}
