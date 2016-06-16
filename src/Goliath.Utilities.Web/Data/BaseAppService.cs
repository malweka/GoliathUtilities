using System;
using System.Collections.Generic;
using Goliath.Data.Entity;
using Goliath.Data.Sql;
using Goliath.Web;
using Goliath.Web.Authorization;

namespace Goliath.Data
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseAppService : IAppService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAppService"/> class.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        protected BaseAppService(ApplicationContext applicationContext)
        {
            CurrentContext = applicationContext;
        }

        /// <summary>
        /// Gets or sets the current application context.
        /// </summary>
        /// <value>
        /// The current application context.
        /// </value>
        public ApplicationContext CurrentContext { get; protected set; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Security.SecurityException">Sign-in is required before performing action;</exception>
        protected UserSession GetCurrentUser()
        {
            var user = CurrentContext.CurrentUser;

            if (CurrentContext.CurrentUser == null)
                throw new System.Security.SecurityException("Sign-in is required before performing action;");

            return user;
        }

        protected ICollection<T> FetchAllFromQuery<T>(IQueryBuilder<T> query, string filter, string sortString = null)
        {
            var filterTuple = CleanFilterString(filter);
            var cleanSort = CleanSortString(sortString);
            ICollection<T> list;

            if (filterTuple != null)
            {
                var unTypeQuery = query.Where(filterTuple.Item1).ILikeValue(filterTuple.Item2);
                if (cleanSort != null)
                {
                    var sortClause = unTypeQuery.OrderBy(cleanSort.Item1);
                    list = cleanSort.Item2 ? sortClause.Desc().FetchAll<T>() : sortClause.Asc().FetchAll<T>();
                }
                else
                {
                    list = query.FetchAll();
                }
            }
            else
            {
                if (cleanSort != null)
                {
                    var sortClause = query.OrderBy(cleanSort.Item1);
                    list = cleanSort.Item2 ? sortClause.Desc().FetchAll() : sortClause.Asc().FetchAll();
                }
                else
                    list = query.FetchAll();
            }

            return list;
        }

        /// <summary>
        /// Fetches the with filter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sortString">The sort string.</param>
        /// <returns></returns>
        protected ICollection<T> FetchAllFromQuery<T>(IQueryBuilder<T> query, int limit, int offset, string filter, string sortString = null)
        {
            var filterTuple = CleanFilterString(filter);
            var cleanSort = CleanSortString(sortString);

            ICollection<T> list;

            if (filterTuple != null)
            {
                var unTypeQuery = query.Where(filterTuple.Item1).ILikeValue(filterTuple.Item2);
                if (cleanSort != null)
                {
                    var sortClause = unTypeQuery.OrderBy(cleanSort.Item1);
                    list = cleanSort.Item2 ? sortClause.Desc().Take(limit, offset).FetchAll<T>() : sortClause.Asc().Take(limit, offset).FetchAll<T>();
                }
                else
                {
                    list = query.Take(limit, offset).FetchAll();
                }
            }
            else
            {
                if (cleanSort != null)
                {
                    var sortClause = query.OrderBy(cleanSort.Item1);
                    list = cleanSort.Item2 ? sortClause.Desc().Take(limit, offset).FetchAll() : sortClause.Asc().Take(limit, offset).FetchAll();
                }
                else
                    list = query.Take(limit, offset).FetchAll();
            }

            return list;
        }

        /// <summary>
        /// Fetches the with filter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="total">The total.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sortString">The sort string.</param>
        /// <returns></returns>
        protected ICollection<T> FetchAllFromQuery<T>(IQueryBuilder<T> query, int limit, int offset, out long total, string filter, string sortString = null)
        {
            var filterTuple = CleanFilterString(filter);
            var cleanSort = CleanSortString(sortString);

            ICollection<T> list;

            if (filterTuple != null)
            {
                var unTypeQuery = query.Where(filterTuple.Item1).ILikeValue(filterTuple.Item2);

                if (cleanSort != null)
                {
                    var sortClause = unTypeQuery.OrderBy(cleanSort.Item1);
                    list = cleanSort.Item2 ? sortClause.Desc().Take(limit, offset).FetchAll<T>(out total) : sortClause.Asc().Take(limit, offset).FetchAll<T>(out total);
                }
                else
                {
                    list = query.Take(limit, offset).FetchAll(out total);
                }
            }
            else
            {
                if (cleanSort != null)
                {
                    var sortClause = query.OrderBy(cleanSort.Item1);
                    list = cleanSort.Item2 ? sortClause.Desc().Take(limit, offset).FetchAll(out total) : sortClause.Asc().Take(limit, offset).FetchAll(out total);
                }
                else
                    list = query.Take(limit, offset).FetchAll(out total);
            }

            return list;
        }


        internal static Tuple<string, bool> CleanSortString(string sortString)
        {
            if (string.IsNullOrWhiteSpace(sortString)) return null;

            var split = sortString.Split(new string[] {"_"}, StringSplitOptions.RemoveEmptyEntries);
            return split.Length > 1 ? Tuple.Create(split[0], split[1].ToUpper().Equals("DESC")) : Tuple.Create(sortString, false);
        }

        internal static Tuple<string, string> CleanFilterString(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;

            var split = filter.Split(new string[] {"_"}, StringSplitOptions.RemoveEmptyEntries);
            return split.Length > 1 ? Tuple.Create(split[0], split[1].Replace("*", "%")) : null;
        }


        /// <summary>
        /// Sets the createable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        protected void SetCreateable<T>(T entity)
        {
            var creatable = entity as ICreatable;
            if (creatable == null) return;

            creatable.CreatedOn = DateTime.UtcNow;
            if (CurrentContext.CurrentUser != null)
                creatable.CreatedBy = CurrentContext.CurrentUser.UserName;
        }

        /// <summary>
        /// Sets the modifiable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        protected void SetModifiable<T>(T entity)
        {
            var modifiable = entity as IModifiable;
            if (modifiable == null) return;

            modifiable.ModifiedOn = DateTime.UtcNow;
            if (CurrentContext.CurrentUser != null)
                modifiable.ModifiedBy = CurrentContext.CurrentUser.UserName;
        }
    }
}