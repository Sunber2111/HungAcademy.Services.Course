﻿using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IBaseServices<TModel, TEntity>
        where TModel : class
        where TEntity : class
    {
        TModel GetById(object id);
        Task<TModel> GetByIdAsync(object id);

        TModel GetById(IUnitOfWork unitOfWork, object id);
        
        Task<TEntity> GetByIdEntityAsync(object id);

        TModel GetFirst(Func<TEntity, bool> filter);
        TModel GetFirst(IUnitOfWork unitOfWork, Func<TEntity, bool> filter);
        TModel GetFirstIgnoreNull(Func<TEntity, bool> filter);
        TModel GetFirstIgnoreNull(IUnitOfWork unitOfWork, Func<TEntity, bool> filter);
        TModel GetFirstWithInclude(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        TModel GetFirstWithIncludeIgnoreNull(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TModel> GetAll();
        Task<List<TModel>> GetAllAsync();

        IEnumerable<TModel> GetAll(IUnitOfWork unitOfWork);
        IEnumerable<TModel> Get(Func<TEntity, bool> filter);
        Task<List<TModel>> GetAsync(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TModel> Get(IUnitOfWork unitOfWork, Func<TEntity, bool> filter);
        IEnumerable<TModel> GetIgnoreNull(Func<TEntity, bool> filter);
        IEnumerable<TModel> GetTop(Func<TEntity, bool> filter, int count, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        IUnitOfWork NewDbContext();

        IQueryable<TEntity> GetEntities(IUnitOfWork unitOfWork);
        IQueryable<TEntity> GetEntities(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> GetEntitiesWithInclude(IUnitOfWork unitOfWork, Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetEntitiesWithInclude(IUnitOfWork unitOfWork, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetEntitiesWithInclude(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);

        TModel Insert(TModel item);
        Task<TModel> InsertAsync(TModel item);
        Task<TModel> InsertAsync(TEntity item);

        TModel Insert(TModel item, IUnitOfWork unitOfWork);
        TEntity InsertEntity(TEntity item);
        Task<TEntity> InsertEntityAsync(TEntity item);
        TEntity InsertEntity(TEntity item, IUnitOfWork unitOfWork);

        IEnumerable<TEntity> InsertEntities(IEnumerable<TEntity> items);
        IEnumerable<TEntity> InsertEntities(IEnumerable<TEntity> items, IUnitOfWork unitOfWork);


        TModel Update(TModel item, object id);
        Task<TModel> UpdateAsync(TModel item, object id);
        TModel Update(TModel item, object id, IUnitOfWork unitOfWork);
        TModel UpdateEntity(TEntity item, object id);
        TModel UpdateEntity(TEntity item, object id, IUnitOfWork unitOfWork);

        bool Delete(object id);
        Task<bool> DeleteAsync(object id);
        bool Delete(object id, IUnitOfWork unitOfWork);

        bool DeleteEntity(TModel entity);
        bool DeleteEntity(TModel entity, IUnitOfWork unitOfWork);

        bool DeleteEntities(IEnumerable<TEntity> entitiesToDelete);

        bool DeleteEntities(IEnumerable<TEntity> entitiesToDelete, IUnitOfWork unitOfWork);

        bool DeleteByEntity(TEntity entity);
        bool DeleteByEntity(TEntity entity, IUnitOfWork unitOfWork);

        bool Exists(object id);

        int ExecuteSqlCommand(string query, params object[] @params);
        int ExecuteSqlCommand(IUnitOfWork unitOfWork, string query, params object[] @params);
        List<T> ExecuteStoreProcedure<T>(string query, params object[] @params) where T : class;

    }
}
