using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service
{
    public class BookService : BaseService, IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IImageRepository imageRepository;
        private readonly IBookPriceRepository bookPriceRepository;
        public BookService(
            IUnitOfWork unitOfWork, 
            IBookRepository bookRepository,
            IMapperCustom mapperCustom,
            IImageRepository imageRepository,
            IBookPriceRepository bookPriceRepository) : base(unitOfWork, mapperCustom)
        {
            this.imageRepository = imageRepository;
            this.bookRepository = bookRepository;
            this.bookPriceRepository = bookPriceRepository;
        }

        public async Task<List<BookViewModel>> GetBooksPaging(int page, int pageSize)
        {
            var req = await bookRepository.GetAllProductPaging(page, pageSize);
            var result = mapperCustom.MapBookPagging(req);
            return result;
        }

        public async Task<BookDetailViewModel> GetBookById(Guid bookId)
        {
            var book = await bookRepository.FindAsync(bk => bk.Id == bookId);
            if (book == null)
            {
                return new BookDetailViewModel()
                {
                    IsSuccess = false,
                    Message = "Can't find Product!"
                };
            }
            var result = mapperCustom.MapBookDetail(book);
            result.IsSuccess = true;
            return result;
        }

        public async Task<BookResponse> AddBook(BookRequest bookReq)
        {
            if (bookReq.Quantity <= 0)
            { 
                return new BookResponse
                {
                    IsSuccess = false,
                    Message = "Can't enter value 0 when adding book!"
                };
            }
            var book = new Book
            {
                CategoryId = bookReq.CategoryId,
                BookName = bookReq.BookName,
                BookDescription = bookReq.BookDescription,
                AuthorId = bookReq.AuthorId,
                NumPage = bookReq.NumPage,
                PublisherId = bookReq.PublisherId,
                Sold = 0,
                DateCreated = DateTime.Now,
                Quantity = bookReq.Quantity,
                IsActive = true
            };
            await bookRepository.AddAsync(book);
            await addImageBook(bookReq.ImageUrls!, book);
            await addBookPrice(bookReq.Price!, book);
            await unitOfWork.CommitTransaction();
            return new BookResponse
            {
                IsSuccess = true,
                Message = "Add book success and you can view this book!",
                Link = "https://localhost:7149/api/Book/" + book.Id.ToString()
            };
        }

        private async Task addImageBook(List<string> images, Book book)
        {
            foreach (var item in images)
            {
                var img = new Image
                {
                   BookId = book.Id,
                   ImageUrl = item
                };
                await imageRepository.AddAsync(img);
            }
        }

        private async Task addBookPrice(BookPriceRequest bookPriceReq, Book book)
        {
            var price = new BookPrice
            {
                BookId = book.Id,
                BookDefaultPrice = bookPriceReq.BookDefautPrice,
                BookSalePrice = bookPriceReq.BookSalePrice,
                ExpirationDate = bookPriceReq.ExpirationDate,
                ActivationDate = bookPriceReq.ActivationDate
            };
            await bookPriceRepository.AddAsync(price);
        }

        public async Task<BookResponse> StopProductionBook(Guid bookId)
        {
            var findBook = await bookRepository.GetQuery(b => b.Id == bookId).SingleAsync();
            if (findBook == null) 
            {
                return new BookResponse
                {
                    IsSuccess = false,
                    Message = "Can't find Book!"
                };
            }
            findBook.IsActive = false;
            bookRepository.Update(findBook);
            await unitOfWork.CommitTransaction();
            return new BookResponse
            {
                IsSuccess = true,
                Message = "Change isActive success!"
            };
        }

        public async Task<BookResponse> UpdateBook(Guid id, BookRequest bookReq)
        {
            var findBook = await bookRepository.GetQuery(b => b.Id == id).SingleAsync();
            if (findBook == null)
            {
                return new BookResponse
                {
                    IsSuccess = false,
                    Message = "Can't find book!"
                };
            }
            throw new NotImplementedException();
        }

        public async Task<List<BookViewModel>> GetBookTopNew()
        {
            var req = await bookRepository.GetTopNewBook();
            return mapperCustom.MapBookPagging(req);
        }

        public async Task<List<BookViewModel>> GetBookBestSeller()
        {
            var req = await bookRepository.GetBestSeller();
            return mapperCustom.MapBookPagging(req);
        }

        public async Task<decimal> NumberOfPages()
        {
            return await bookRepository.NumberOfPages();
        }
    }
}
