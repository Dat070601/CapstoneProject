using BookStore.Models.DataViewModel;

namespace BookStore.Service.Interfaces
{
    public interface IBookService
    {
        Task<List<BookViewModel>> GetBooksPaging(int page, int pageSize);
        Task<BookDetailViewModel> GetBookById(Guid bookId);
    }
}
