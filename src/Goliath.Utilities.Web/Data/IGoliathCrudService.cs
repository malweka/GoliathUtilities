using System.Collections.Generic;

namespace Goliath.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdType">The type of the identifier type.</typeparam>
    public interface IGoliathCrudService<T, in TIdType>
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Create(T entity);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(TIdType id);

        /// <summary>
        /// Fetches the one.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T FetchOne(TIdType id);

        /// <summary>
        /// Fetches all.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="total">The total.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sortString">The sort string.</param>
        /// <returns></returns>
        ICollection<T> FetchAll(int limit, int offset, out long total, string filter = null, string sortString = null);


        /// <summary>
        /// Fetches all.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sortString">The sort string.</param>
        /// <returns></returns>
        ICollection<T> FetchAll(int limit, int offset, string filter = null, string sortString = null);


        /// <summary>
        /// Fetches all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="sortString">The sort string.</param>
        /// <returns></returns>
        ICollection<T> FetchAll(string filter = null, string sortString = null);
    }
}