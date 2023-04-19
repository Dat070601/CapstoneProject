using BookStore.Models.Entities;

namespace BookStore.Models.DAL.Interfaces
{
    public interface ICartDetailRepository : IRepository<CartDetail>
    {
        Task<List<CartDetail>> GetListCartDetailByCartId(Guid id);
        Task<CartDetail> GetCartDetailByCartIdAndBookId(Guid cartId, Guid bookId);
    }
}
