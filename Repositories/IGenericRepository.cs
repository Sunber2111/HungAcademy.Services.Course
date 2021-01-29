using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetMany(Func<TEntity, bool> where);

        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where);

        TEntity GetFirst(Func<TEntity, bool> predicate);

        TEntity GetFirstOrDefault(Func<TEntity, Boolean> where);

        TEntity GetById(object id);

        TEntity GetSingle(Func<TEntity, bool> predicate);

        IQueryable<TEntity> GetEntitiesWithInclude(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        
        IQueryable<TEntity> GetEntitiesWithInclude(params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> GetEntities();

        TEntity Find(object id);

        Task<TEntity> FindAsync(object id);

        TEntity Insert(TEntity entityToInsert);

        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entitiesToInsert);

        TEntity Update(TEntity entityToUpdate);

        bool Delete(object id);

        Task<bool> DeleteAsync(object id);

        bool Delete(TEntity entityToDelete);

        void DeleteRange(IEnumerable<TEntity> entitiesToDelete);

        bool Delete(Func<TEntity, Boolean> where);

        bool Exists(object primaryKey);

        Task<TEntity> GetByIdAsync(params Object[] keyValues);

        int ExecuteSqlCommand(string query, params object[] @params);

        List<T> FromSql<T>(string query, params object[] @params) where T : class;
    }
}
