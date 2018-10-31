using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Add an entity as new
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Modify an entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Remove an entity by entity injected
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// Remove an entity by id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Delete multi entities
        /// </summary>
        /// <param name="where"></param>
        void DeleteMulti(Expression<Func<T, bool>> where);

        /// <summary>
        /// Get an Entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetSingleEntity(int id);
        /// <summary>
        /// Get an Entity by condition
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetSingleEntity(Expression<Func<T, bool>> expression, string[] includes = null);

        /// <summary>
        /// Get all entity markup
        /// </summary>
        /// <param name="included"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(string[] included = null);

        /// <summary>
        /// Get multiple entities by condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        /// <summary>
        /// Paging enities
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="total"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="includeds"></param>
        /// <returns></returns>
        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includeds = null);

        /// <summary>
        /// Count number but matched condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> where);

        /// <summary>
        /// Check whether an entity contain thing matched condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}
