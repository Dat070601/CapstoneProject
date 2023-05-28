using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Service
{
    public class CartService : BaseService, ICartService
    {
        private readonly ILogger<CartService> logger;
        private readonly IBookRepository bookRepository;
        private readonly ICartRepository cartRepository;
        private readonly ICartDetailRepository cartDetailRepository;
        public CartService(
            IUnitOfWork unitOfWork,
            IMapperCustom mapperCustom,
            ILogger<CartService> logger,
            IBookRepository bookRepository,
            ICartRepository cartRepository,
            ICartDetailRepository cartDetailRepository) : base(unitOfWork, mapperCustom)
        {
            this.logger = logger;
            this.bookRepository = bookRepository;
            this.cartRepository = cartRepository;
            this.cartDetailRepository = cartDetailRepository;
        }

        public async Task<CartResponse> UpdateCart(CartRequest cartReq, Guid cusId)
        {
            try
            {
                var findCart = await cartRepository.GetQuery(c => c.AccountId == cusId).SingleAsync();
                var findCartDetail = await cartDetailRepository.GetQuery(cd => cd.CartId == findCart.Id).ToListAsync();
                foreach (var item in findCartDetail)
                {
                    if (item.BookId == cartReq.BookId)
                    {
                        item.Quantity = cartReq.Quantity;
                        cartDetailRepository.Update(item);
                    }
                }
                await unitOfWork.CommitTransaction();
                return new CartResponse
                {
                    IsSuccess = true,
                    Message = "Update Cart success!"
                };
            }
            catch (InvalidOperationException e)
            {
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "Some properties is valid !",
                };
            }
            catch (Exception e)
            {
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "System error, Add Product to Cart was fasle and please try again later! "
                };
            }
        }

        public async Task<CartResponse> DeleteCart(List<CartOptionRequest> cartReq, Guid cusId)
        {
            try
            {
                var findCart = await cartRepository.GetQuery(c => c.AccountId == cusId).SingleAsync();
                var findCartDetail = await cartDetailRepository.GetQuery(cd => cd.CartId == findCart.Id).ToListAsync();
                foreach (var item in findCartDetail)
                {
                    foreach (var cart in cartReq)
                    {
                        if(item.BookId == cart.BookId)
                        {
                            cartDetailRepository.Delete(item);
                        }
                    }
                }
                await unitOfWork.CommitTransaction();
                return new CartResponse
                {
                    IsSuccess = true,
                    Message = "Delete Cart success!"
                };
            }
            catch (InvalidOperationException e)
            {
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "Some properties is valid !",
                };
            }
            catch (Exception e)
            {
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "System error, Add Product to Cart was fasle and please try again later! "
                };
            }
        }

        public async Task<CartResponse> DeleteProductCart(CartRequest req, Guid id)
        {
            try
            {
                if (!checkQuantities(req).Result.IsSuccess)
                {
                    return checkQuantities(req).Result;
                }
                var findCart = await cartRepository.GetCartByCustomerId(id);
                var listCartDetail = await cartDetailRepository.GetListCartDetailByCartId(findCart.Id);
                foreach (var item in listCartDetail)
                {
                    if(item.BookId == req.BookId)
                    {
                        item.Quantity -= req.Quantity;
                        await unitOfWork.CommitTransaction();
                        return new CartResponse
                        {
                            IsSuccess = true,
                            Message = "Delete product success!"
                        };
                    }
                }
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "System error, Delete product from cart was fasle and please try again later! "
                };
            }
            catch (InvalidOperationException e)
            {
                logger.LogError($"{e.Message}. Detail {e.StackTrace}");
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "Some properties is valid !",
                };
            }
            catch (Exception e)
            {
                logger.LogError($"{e.Message}. Detail {e.StackTrace}");
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "System error, Delete product from cart was fasle and please try again later!! "
                };
            }
        }

        public async Task<CartResponse> AddCart(CartRequest req, Guid Id)
        {
            try
            {
                if (!checkQuantities(req).Result.IsSuccess)
                {
                    return checkQuantities(req).Result;
                }
                var findCart = await cartRepository.GetCartByCustomerId(Id);
                var listCartDetail = await cartDetailRepository.GetListCartDetailByCartId(findCart.Id);
                foreach (var item in listCartDetail)
                {
                    if (item.BookId == req.BookId)
                    {
                        item.Quantity += req.Quantity;
                        await unitOfWork.CommitTransaction();
                        return new CartResponse
                        {
                            IsSuccess = true,
                            Message = "Increased the number of this product in the cart Success!"
                        };
                    }
                }
                var createCartDetail = new CartDetail
                {
                    BookId = req.BookId,
                    Quantity = req.Quantity,
                    CartId = findCart.Id,
                };
                await cartDetailRepository.AddAsync(createCartDetail);
                await unitOfWork.CommitTransaction();
                return new CartResponse
                {
                    IsSuccess = true,
                    Message = "Add Product To Cart Success!"
                };
            }
            catch (InvalidOperationException e)
            {
                logger.LogError($"{e.Message}. Detail {e.StackTrace}");
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "Some properties is valid !",
                };
            }
            catch (Exception e)
            {
                logger.LogError($"{e.Message}. Detail {e.StackTrace}");
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "System error, Add Product to Cart was fasle and please try again later! "
                };
            }
        }

        public async Task<CartResponse> GetCart(Guid Id)
       {
            var findCart = await cartRepository.GetCartByCustomerId(Id);
            var listCartDetail = await cartDetailRepository.GetListCartDetailByCartId(findCart.Id);
            var result = new CartResponse();
            foreach (var item in listCartDetail)
            {
                if(item.Book.BookPrice.ExpirationDate < DateTime.Now)
                {
                    var cartDetail = new CartDetailViewModel
                    {
                        BookId = item.BookId,
                        BookName = item.Book.BookName,
                        ImageUrl = mapperCustom.MapImages(item.Book.Images.ToList())[0].ImageUrl,
                        Price = item.Book.BookPrice.BookSalePrice,
                        Quantity = item.Quantity,
                        TotalPrice = item.Book.BookPrice.BookSalePrice * item.Quantity
                    };
                    result.cartDetailViewModels!.Add(cartDetail);
                }
                else
                {
                    var cart = new CartDetailViewModel
                    {
                        BookId = item.BookId,
                        BookName = item.Book.BookName,
                        ImageUrl = mapperCustom.MapImages(item.Book.Images.ToList())[0].ImageUrl,
                        Price = item.Book.BookPrice.BookDefaultPrice,
                        Quantity = item.Quantity,
                        TotalPrice = item.Book.BookPrice.BookDefaultPrice * item.Quantity
                    };
                    result.cartDetailViewModels!.Add(cart);
                }
            }
            return result;
        }

        private async Task<CartResponse> checkQuantities(CartRequest req)
        {
            if (req.Quantity <= 0)
            {
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "Quantity must be greater than 0!"
                };
            }
            var findProduct = await bookRepository.FindAsync(bk => bk.Id == req.BookId);
            if (findProduct == null)
            {
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "Can't find product!"
                };
            }
            if (findProduct.Quantity < req.Quantity)
            {
                return new CartResponse
                {
                    IsSuccess = false,
                    Message = "Can't add to cart more than available quantity!"
                };
            }
            return new CartResponse
            {
                IsSuccess = true,
            };
        }
    }
}
