using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wheely.Core.Entities.Abstract;

namespace Wheely.Data.Abstract.Repositories
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        #region Methods
        /// <summary>
        /// Get entities
        /// </summary>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entities</returns>
        List<TEntity> GetAll(bool disableTracking = true);

        /// <summary>
        /// Get entities by expression fiter
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entities</returns>
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, bool disableTracking = true);

        /// <summary>
        /// Get entities by expression fiter with ordering
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="orderBy">orderby filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entities</returns>
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true);

        /// <summary>
        /// Get entities by expression fiter with ordering
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="orderBy">orderby filter</param>
        /// <param name="includeProperties">include string filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entities</returns>
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true, params string[] includeProperties);

        /// <summary>
        /// Get entity by expression fiter
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entity</returns>
        TEntity Get(Expression<Func<TEntity, bool>> filter, bool disableTracking = true);

        /// <summary>
        /// Get entity by expression fiter
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="orderBy">orderby filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entity</returns>
        TEntity Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true);

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Is there by expression filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns></returns>
        bool AnyFilter(Expression<Func<TEntity, bool>> filter, bool disableTracking = true);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entity">Entity</param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void InsertRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entity">Entity</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void DeleteRange(IEnumerable<TEntity> entities);
        #endregion

        #region Properties
        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }
        #endregion
    }
}
