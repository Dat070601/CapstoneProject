using BookStore.Models.DAL;
using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class CartService : BaseService, ICartService
    {
        private readonly ILogger logger;
        private readonly IBookRepository bookRepository;
        public CartService(
            IUnitOfWork unitOfWork,
            IMapperCustom mapperCustom,
            ILogger logger,
            IBookRepository bookRepository) : base(unitOfWork, mapperCustom)
        {
            this.logger = logger;
            this.bookRepository = bookRepository;
        }

        private async Task<CartResponse> checkQuantities(CartRequest req)
        {
            var findProduct = await bookRepository.FindAsync(bk => bk.Id == req.BookId);
            if (findProduct == null)
            {
                return new CartResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Can't find product!"
                };
            }
            if (findProduct.Quantity < req.Quantity)
            {
                return new CartResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Can't add to cart more than available quantity!"
                };
            }
            return new CartResponse
            {
                IsSuccess = true,
            };
        }
        public async Task<CartResponse> AddCart(CartRequest req)
        {
            try
            {
                if (!checkQuantities(req).Result.IsSuccess)
                {
                    return checkQuantities(req).Result;
                }
                return checkQuantities(req).Result;
            //    var findCart = await cartRepository.GetCartByCustomerId(cusId);
            //    var findCartDetail = await cartDetailRepository.GetListCartDetailByCartId(findCart.CartId);
            //    foreach (var item in findCartDetail)
            //    {
            //        if (item.ProductVariantId.Equals(req.ProductVariantId))
            //        {
            //            item.Quantity += req.Quantity;
            //            await unitOfWork.CommitTransaction();
            //            return new CartResponse
            //            {
            //                IsSuccess = true,
            //                Message = "Increased the number of this product in the cart Success!"
            //            };
            //        }
            //    }
            //    var createCartDetail = new CartDetail
            //    {
            //        ProductVariantId = req.ProductVariantId,
            //        Quantity = req.Quantity,
            //        CartId = findCart.CartId,
            //    };
            //    await cartDetailRepository.AddAsync(createCartDetail);
            //    await _unitOfWork.CommitTransaction();
            //    return new CartResponse
            //    {
            //        IsSuccess = true,
            //        Message = "Add Product To Cart Success!"
            //    };
            //}
            //catch (InvalidOperationException e)
            //{
            //    logger.LogError($"{e.Message}. Detail {e.StackTrace}");
            //    return new CartResponse
            //    {
            //        IsSuccess = false,
            //        Message = "Some properties is valid !",
            //    };
            }
            catch (Exception e)
            {
                logger.LogError($"{e.Message}. Detail {e.StackTrace}");
                return new CartResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "System error, Add Product to Cart was fasle and please try again later! "
                };
            }
        }
    }
}
