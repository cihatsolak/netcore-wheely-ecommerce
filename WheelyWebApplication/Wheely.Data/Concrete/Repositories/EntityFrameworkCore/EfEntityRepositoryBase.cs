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
        public virtual List<TEntity> GetAll(bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;
            return entityTable.ToList();
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;

            if (filter is not null)
                entityTable = entityTable.Where(filter);

            return entityTable.ToList();
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;

            if (filter is not null)
                entityTable = entityTable.Where(filter);

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

           return entityTable.ToList();
        }

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

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;
            return entityTable.FirstOrDefault(filter);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;

            if (orderBy is not null)
                entityTable = orderBy(entityTable);

            return entityTable.FirstOrDefault(filter);
        }

        public virtual TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual bool AnyFilter(Expression<Func<TEntity, bool>> filter, bool disableTracking = true)
        {
            IQueryable<TEntity> entityTable = disableTracking ? TableNoTracking : Table;
            return entityTable.Any(filter);
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Add(entity);
            SaveChanges();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await Entities.AddAsync(entity);
            await SaveChangesAsync();
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            Entities.AddRange(entities);
            SaveChanges();
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            await Entities.AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Update(entity);
            SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            Entities.UpdateRange(entities);
            SaveChanges();
        }

        /// <summary>
        ///  Delete entity
        /// </summary>
        /// <param name="id">primary key</param>
        public virtual void Delete(int id)
        {
            Delete(GetById(id));
            SaveChanges();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            Entities.RemoveRange(entities);
            SaveChanges();
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

        protected virtual void SaveChanges()
        {
            try
            {
               _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        protected virtual async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        #endregion
    }
}
