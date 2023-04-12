using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.DAL
{
    public class DbFactory : IDisposable
    {
        private bool disposed;
        private Func<BookStoreContext> instanceFunc;
        private BookStoreContext dbContext;
        public BookStoreContext DbContext => dbContext ?? (dbContext = instanceFunc.Invoke());

        public DbFactory(Func<BookStoreContext> dbContextFactory)
        {
            instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if(!disposed && dbContext != null)
            {
                disposed = true;
                dbContext.Dispose();
            }
        }
    }
}
