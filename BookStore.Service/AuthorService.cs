using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service
{
    public class AuthorService : BaseService, IAuthorService
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;
        public AuthorService(
            IUnitOfWork unitOfWork, 
            IMapperCustom mapperCustom,
            IAuthorRepository authorRepository,
            IBookRepository bookRepository) : base(unitOfWork, mapperCustom)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        public async Task<List<BookViewModel>> GetBookByAuthorId(Guid authorId)
        {
            var listBook = await bookRepository.GetQuery(b => b.AuthorId == authorId).ToListAsync();
            return mapperCustom.MapBookPagging(listBook);
        }

        public async Task<List<AuthorViewModel>> GetListAuthor()
        {
            var listAuthor = await authorRepository.GetAll();
            return mapperCustom.MapAuthors(listAuthor);
        }
    }
}
