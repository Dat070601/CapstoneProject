using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
 
namespace BookStore.Service
{
    public class ReviewService : BaseService, IReviewService
    {
        private readonly IReviewRepository reviewRepository;
        public ReviewService(
            IUnitOfWork unitOfWork, 
            IMapperCustom mapperCustom,
            IReviewRepository reviewRepository) : base(unitOfWork, mapperCustom)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<ReviewResponse> AddReview(ReviewRequest reviewReq, Guid cusId)
        {
            var review = new Review
            {
                AccountId = cusId,
                BookId = reviewReq.BookId,
                ReviewText = reviewReq.ReviewText,
            };
            await reviewRepository.AddAsync(review);
            return new ReviewResponse
            {
                IsSuccess = true,
                Message = "Add comment success!!"
            };
        }

        public async Task<ReviewResponse> DeleteReview(DeleteReviewRequest reviewReq, Guid cusId)
        {
            var findReview = await reviewRepository.GetQuery(rv => rv.Id == reviewReq.ReviewId).SingleAsync();
            if(findReview.AccountId != cusId)
            {
                return new ReviewResponse
                {
                    IsSuccess = false,
                    Message = "You do not have permission to delete this comment!!"
                };
            }
            reviewRepository.Delete(findReview);
            await unitOfWork.CommitTransaction();
            return new ReviewResponse
            {
                IsSuccess = true,
                Message = "Delete review success!!"
            };
        }

        public async Task<ReviewResponse> UpdateReview(UpdateReviewRequest reviewReq, Guid cusId)
        {
            var findReview = await reviewRepository.GetQuery(rv => rv.Id == reviewReq.ReviewId).SingleAsync();
            if (findReview.AccountId != cusId)
            {
                return new ReviewResponse
                {
                    IsSuccess = false,
                    Message = "You do not have permission to update this comment!!"
                };
            }
            findReview.ReviewText = reviewReq.ReviewText;
            reviewRepository.Update(findReview);
            await unitOfWork.CommitTransaction();
            return new ReviewResponse
            {
                IsSuccess = true,
                Message = "Update review success!!"
            };
        }
    }
}
