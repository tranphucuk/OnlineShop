using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T>  where T : class
    {
        #region Properties
        private OnlineShopDbContext dataContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected OnlineShopDbContext DbContext
        {
            get
            {
                return dataContext ?? (dataContext = new OnlineShopDbContext());
            }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        /// <summary>
        /// Add an entity as new
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Modify an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Delete multi entities
        /// </summary>
        /// <param name="where"></param>
        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (var item in objects)
            {
                dbSet.Remove(item);
            }
        }

        /// <summary>
        /// Get an Entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetSingleEntity(int id)
        {
            return dbSet.Find(id);
        }
        /// <summary>
        /// Get an Entity by condition
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual T GetSingleEntity(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            return GetAll(includes).FirstOrDefault(expression);
        }

        ///// <summary>
        ///// Get multiple entities by condition
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <param name="includes"></param>
        ///// <returns></returns>
        //public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includes)
        //{
        //    return dbSet.Where(where).ToList();
        //}

        /// <summary>
        /// Count number but matched condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }

        /// <summary>
        /// Get all entity markup
        /// </summary>
        /// <param name="included"></param>
        /// <returns></returns>
        public IQueryable<T> GetAll(string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);

                }
                return query.AsQueryable();
            }
            return dataContext.Set<T>().AsQueryable();
        }

        /// <summary>
        /// Paging enities
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="total"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="includeds"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 20, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? dataContext.Set<T>().Where<T>(predicate).AsQueryable() : dataContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        /// <summary>
        /// Check whether an entity has property matched condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return dataContext.Set<T>().Count<T>(predicate) > 0;
        }

        /// <summary>
        /// Get Multi Entity By conditions
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return dataContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }
    }
}
