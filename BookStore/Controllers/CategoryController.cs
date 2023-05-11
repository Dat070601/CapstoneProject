using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Service;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetBookByCategory(Guid categoryId)
        {
            var res = await categoryService.GetBookByCategoryId(categoryId);
            return Ok(res);
        }


        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequest cateReq)
        {
            var res = await categoryService.AddCategory(cateReq);
            return Ok(res);
        }

        //[Authorize]
        [HttpPost("subCate")]
        public async Task<IActionResult> AddSubCate([FromBody] SubCategoryRequest cateReq)
        {
            var res = await categoryService.AddSubCateogry(cateReq);
            return Ok(res);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] SubCategoryRequest cateReq, Guid idCate)
        {
            var res = await categoryService.UpdateNameCategory(cateReq, idCate);
            return res.IsSuccess ? Ok(res) : BadRequest(res.Message);
        }
    }
}
