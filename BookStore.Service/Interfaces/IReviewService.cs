using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;

namespace BookStore.Service.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewResponse> AddReview(ReviewRequest reviewReq, Guid cusId);
        Task<ReviewResponse> DeleteReview(DeleteReviewRequest reviewReq, Guid cusId);
        Task<ReviewResponse> UpdateReview(UpdateReviewRequest reviewReq, Guid cusId);


    }
}
