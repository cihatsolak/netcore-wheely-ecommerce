using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wheely.Core.Entities.Abstract;
using Wheely.Data.Abstract.Repositories.EntityFrameworkCore;

namespace Wheely.Data.Concrete.Repositories.EntityFrameworkCore
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity, TContext> where TEntity : class, IEntity, new()
        where TContext : DbContext  
    {
        #region Fields
        private readonly TContext _context;
        private DbSet<TEntity> _entities;
        #endregion

        #region Constructors
        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
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
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await Entities.AddAsync(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            Entities.AddRange(entities);
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            await Entities.AddRangeAsync(entities);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            Entities.UpdateRange(entities);
        }

        /// <summary>
        ///  Delete entity
        /// </summary>
        /// <param name="id">primary key</param>
        public virtual void Delete(int id)
        {
            Delete(GetById(id));
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
    }
}
