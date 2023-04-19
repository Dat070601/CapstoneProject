using BookStore.Models.DataViewModel;

namespace BookStore.Service.Interfaces
{
    public interface ISearcbService
    {
        Task<List<BookViewModel>> SearchBookWithFuzzy(string nameBook);
    }
}
