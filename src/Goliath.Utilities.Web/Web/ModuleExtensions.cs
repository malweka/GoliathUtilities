using System;
using Goliath.Data.Diagnostics;
using Goliath.Models;
using Goliath.Web.Authorization;
using Nancy;

namespace Goliath.Web
{
    public static class ModuleHelpers
    {
        /// <summary>
        /// Gets the paging query information.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">request</exception>
        public static PagingQueryInfo GetPagingQueryInfo(this Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            int limit;
            int offset;
            int totalPages;

            int.TryParse(request.Query.limit, out limit);
            int.TryParse(request.Query.offset, out offset);
            int.TryParse(request.Query.pages, out totalPages);

            var currentPage = 1;
            if (limit > 0)
            {
                currentPage = offset / limit;
            }

            if (offset < 1)
                offset = 0;

            string filter = null;
            string sortString = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(request.Query.ft))
                {
                    filter = request.Query.ft.ToString();
                }

                if (!string.IsNullOrWhiteSpace(request.Query.srt))
                {
                    sortString = request.Query.srt.ToString();
                }
            }
            catch
            {

            }

            return new PagingQueryInfo { TotalPages = totalPages, Limit = limit, Offset = offset, CurrentPage = currentPage, Filter = filter, SortString = sortString };
        }

        //public static Response ErrorResponse(this NancyModule module, Exception exception)
        //{
        //    var response = (Response) HttpStatusCode.InternalServerError;

        //}
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="exception">The exception.</param>
        /// <exception cref="System.ArgumentNullException">logger</exception>
        public static void LogException(this NancyModule module, ILogger logger, string exceptionMessage, Exception exception)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            //if (exception == null) throw new ArgumentNullException("exception");

            if (module.Context.CurrentUser != null)
            {
                var user = module.Context.CurrentUser as UserSession;

                if (user != null)
                {
                    logger.LogException(user.SessionId, exceptionMessage, exception);
                }
            }

            logger.LogException(exceptionMessage, exception);
        }

        /// <summary>
        /// Getints the parameter.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static int GetintIdParameter(dynamic parameters)
        {
            int intV;
            int.TryParse(parameters.Id, out intV);
            return intV;
        }

        /// <summary>
        /// Getlongs the parameter.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static long GetlongIdParameter(dynamic parameters)
        {
            long intV;
            long.TryParse(parameters.Id, out intV);
            return intV;
        }
    }
}