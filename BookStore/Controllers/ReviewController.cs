using BookStore.Models.DataViewModel.Requests;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;
        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest reviewReq)
        {
            var res = await reviewService.AddReview(reviewReq, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewRequest reviewReq)
        {
            var res = await reviewService.UpdateReview(reviewReq, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReview([FromBody] DeleteReviewRequest reviewReq)
        {
            var res = await reviewService.DeleteReview(reviewReq, new Guid("8F330FA6-0551-440C-A02F-2AE608BD97CE"));
            return Ok(res);
        }
    }
}
