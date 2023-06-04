using BookStore.Models.DataViewModel;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;

namespace BookStore.Service.Interfaces
{
    public interface IBookService
    {
        Task<List<BookViewModel>> GetBooksPaging(int page, int pageSize);
        Task<int> GetCountBook();
        Task<List<BookViewModel>> GetBookTopNew(); 
        Task<List<BookViewModel>> GetBookBestSeller();
        Task<BookDetailViewModel> GetBookById(Guid bookId);
        Task<BookResponse> AddBook(BookRequest bookReq);
        Task<BookResponse> StopProductionBook(Guid bookId);
        Task<BookResponse> UpdateBook(Guid id,BookRequest bookReq);
        Task<List<BookViewModel>> GetFourBook(Guid bookId);
        Task<decimal> NumberOfPages();
    }
}
