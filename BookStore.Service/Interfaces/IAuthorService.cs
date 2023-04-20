using BookStore.Models.DataViewModel;

namespace BookStore.Service.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorViewModel>> GetListAuthor();
        Task<List<BookViewModel>> GetBookByAuthorId(Guid authorId);
    }
}
