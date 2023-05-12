using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Service;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await categoryService.AddCategory(cateReq);
                return Ok(res);
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
        [HttpPost("subCate")]
        public async Task<IActionResult> AddSubCate([FromBody] SubCategoryRequest cateReq)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await categoryService.AddSubCateogry(cateReq);
                return Ok(res);
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
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] SubCategoryRequest cateReq, Guid idCate)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var handler = new JwtSecurityTokenHandler();
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                var tokenString = handler.ReadToken(token) as JwtSecurityToken;
                var userId = new Guid(tokenString!.Claims.First(token => token.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
                var res = await categoryService.UpdateNameCategory(cateReq, idCate);
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
    }
}
