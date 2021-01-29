using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        DbContext GetDbContext();
        bool Save();
        Task<bool> SaveChangeAsync();
        IGenericRepository<T> Repository<T>() where T : class;
    }
}
