using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Feild
        private bool disposed;
        private Dictionary<Type, object> repositories;
        private IDbContextTransaction transactionScope;
        protected DbContext context;
        #endregion

        public UnitOfWork(DbContext context)
        {
            this.context = context;
            repositories = new Dictionary<Type, object>();
        }

        public void BeginTransaction()
        {
            if (transactionScope == null)
            {
                transactionScope = context.Database.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            if (transactionScope != null)
            {
                transactionScope.Commit();
                transactionScope.Dispose();
                transactionScope = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (repositories != null)
                    {
                        repositories.Clear();
                        repositories = null;
                    }

                    if (transactionScope != null)
                    {
                        transactionScope.Commit();
                        transactionScope.Dispose();
                        transactionScope = null;
                    }

                    if (context != null)
                    {
                        context.Dispose();
                        context = null;
                    }
                }
            }

            this.disposed = true;
        }

        public DbContext GetDbContext()
        {
            return context;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            IGenericRepository<T> repository = null;

            if (repositories.ContainsKey(typeof(T)))
            {
                repository = repositories[typeof(T)] as IGenericRepository<T>;
            }
            else
            {
                repository = new GenericRepository<T>(context);
                repositories.Add(typeof(T), repository);
            }

            return repository;
        }

        public void RollbackTransaction()
        {
            if (transactionScope != null)
            {
                transactionScope.Rollback();
                transactionScope.Dispose();
                transactionScope = null;
            }
        }

        public bool Save()
        {
            try
            {
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SaveChangeAsync()
        {
            try
            {
                return (await context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
