﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.UnitOfWork
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region GetPagedList

        IPagedList<TEntity> GetPagedList(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        Task<IPagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false);

        IPagedList<TResult> GetPagedList<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false) where TResult : class;

        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false) where TResult : class;

        #endregion

        #region GetFirstOrDefault

        TEntity? GetFirstOrDefault(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        TResult? GetFirstOrDefault<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        Task<TResult?> GetFirstOrDefaultAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        #endregion

        #region Find

        TEntity? Find(params object[] keyValues);

        ValueTask<TEntity?> FindAsync(params object[] keyValues);

        ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken);

        #endregion

        #region GetAll

        IQueryable<TEntity> GetAll(bool disableTracking = true);

        IQueryable<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            bool disableTracking = true);

        IQueryable<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            bool disableTracking = true);

        IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        IQueryable<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        Task<IList<TEntity>> GetAllAsync(bool disableTracking = true);

        Task<IList<TResult>> GetAllAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            bool disableTracking = true);

        Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        Task<IList<TResult>> GetAllAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        #endregion

        #region Insert

        TEntity Insert(TEntity entity);

        void Insert(params TEntity[] entities);

        void Insert(IEnumerable<TEntity> entities);

        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task InsertAsync(params TEntity[] entities);

        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Update

        void Update(TEntity entity);

        void Update(params TEntity[] entities);

        void Update(IEnumerable<TEntity> entities);

        #endregion

        #region Delete

        void Delete(object id);

        void Delete(TEntity entity);

        void Delete(params TEntity[] entities);

        void Delete(IEnumerable<TEntity> entities);

        #endregion

        #region Count

        int Count(Expression<Func<TEntity, bool>>? predicate = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

        long LongCount(Expression<Func<TEntity, bool>>? predicate = null);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

        #endregion

        #region Exists

        bool Exists(Expression<Func<TEntity, bool>>? predicate = null);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null, CancellationToken cancellationToken = default);

        #endregion

        #region Aggregations

        T? Max<T>(Expression<Func<TEntity, T>> selector, Expression<Func<TEntity, bool>>? predicate = null);

        Task<T> MaxAsync<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        T? Min<T>(Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null);

        Task<T> MinAsync<T>(
            Expression<Func<TEntity, T>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        decimal Average(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null);

        Task<decimal> AverageAsync(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        decimal Sum(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>>? predicate = null);

        Task<decimal> SumAsync(
            Expression<Func<TEntity, decimal>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region Other

        void ChangeEntityState(TEntity entity, EntityState state);

        void ChangeTable(string table);

        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        #endregion
    }
}