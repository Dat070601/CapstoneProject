namespace BookStore.Models.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
