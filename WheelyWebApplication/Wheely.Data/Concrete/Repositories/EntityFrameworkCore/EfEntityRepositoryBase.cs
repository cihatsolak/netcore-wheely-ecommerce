using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wheely.Core.Entities.Abstract;
using Wheely.Data.Abstract.Repositories;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Concrete.Repositories.EntityFrameworkCore
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        #region Fields
        private readonly WheelDbContext _context;
        private DbSet<TEntity> _entities;
        #endregion

        #region Constructors
        public EfEntityRepositoryBase(WheelDbContext wheelDbContext)
        {
            _context = wheelDbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get entities
        /// </summary>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entities</returns>
        public virtual List<TEntity> GetAll(bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;
            return entityTable.ToList();
        }

        /// <summary>
        /// Get entities by expression fiter
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entities</returns>
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;

            if (filter is not null)
                entityTable = entityTable.Where(filter);

            return entityTable.ToList();
        }

        /// <summary>
        /// Get entities by expression fiter with ordering
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="orderBy">orderby filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;

            if (filter is not null)
                entityTable = entityTable.Where(filter);

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

           return entityTable.ToList();
        }

        /// <summary>
        /// Get entities by expression fiter with ordering
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="orderBy">orderby filter</param>
        /// <param name="includeProperties">include string filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entities</returns>
        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true, params string[] includeProperties)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;

            if (filter is not null)
                entityTable = entityTable.Where(filter);

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

            if (includeProperties is not null)
            {
                foreach (var property in includeProperties)
                    entityTable = entityTable.Include(property);
            }

            return entityTable.ToList();
        }

        /// <summary>
        /// Get entity by expression fiter
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entity</returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;
            return entityTable.FirstOrDefault(filter);
        }

        /// <summary>
        /// Get entity by expression fiter
        /// </summary>
        /// <param name="filter">expresssion filter</param>
        /// <param name="orderBy">orderby filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns>Entity</returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

            return entityTable.FirstOrDefault(filter);
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        /// <summary>
        /// Is there by expression filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="disableTracking">entity state tracking filter</param>
        /// <returns></returns>
        public virtual bool IsThere(Expression<Func<TEntity, bool>> filter, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;
            return entityTable.Any(filter);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await Entities.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.AddRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                await Entities.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.UpdateRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a table
        /// </summary>
        public IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }
        #endregion

        #region Utilities

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                _context.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString();
            }
        }

        #endregion
    }
}
