using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;

namespace BookStore.Service
{
    public class BookService : BaseService, IBookService
    {
        private readonly IBookRepository bookRepository;
        public BookService(
            IUnitOfWork unitOfWork, 
            IBookRepository bookRepository,IMapperCustom mapperCustom) : base(unitOfWork, mapperCustom)
        {
            this.bookRepository = bookRepository;
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
    }
}
