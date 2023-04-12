using BookStore.Models.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace BookStore.Models.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreContext dbContext;
        private IDbContextTransaction transaction;
        private readonly System.Data.IsolationLevel isolationLevel;
        private readonly DbFactory? dbFactory;
        public bool HasActiveTransaction => transaction != null;

        public UnitOfWork(DbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }
        
        public Task CommitTransaction()
        {
            return dbFactory!.DbContext.SaveChangesAsync();
        }

        public Task RollbackTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
