using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;

namespace BookStore.Service
{
    public class SearchService : BaseService, ISearcbService
    {
        private readonly IBookRepository bookRepository;
        public SearchService(
            IUnitOfWork unitOfWork, 
            IMapperCustom mapperCustom,
            IBookRepository bookRepository) : base(unitOfWork, mapperCustom)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<List<BookViewModel>> SearchBookWithFuzzy(string nameBook)
        {
            if(nameBook == null)
            {
                return null!;
            }
            var listBook = await bookRepository.SearchBookWithFuzzy(nameBook);
            return mapperCustom.MapBookPagging(listBook);            
        }
    }
}
