using System;
using Nancy;
using Nancy.Responses;
using Nancy.ViewEngines.Razor;

namespace Goliath.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Retrieves the localized absolute url of the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string ContentLocalized<T>(this UrlHelpers<T> urlHelper, string path)
        {
            var currentCulture = urlHelper.RenderContext.Context.Culture;
            if (string.IsNullOrWhiteSpace(path) || currentCulture == null)
                return urlHelper.Content(path);

            if (currentCulture.Name.StartsWith("en", StringComparison.InvariantCultureIgnoreCase) || currentCulture.Name.Equals("en"))
                return urlHelper.Content(path);

            string currName;
            if(currentCulture.Name.Length>2)
             currName = currentCulture.Name.Substring(0, 2);
            else
                currName = currentCulture.Name;
            

            if (path.StartsWith("/", StringComparison.InvariantCultureIgnoreCase))
            {
                var localizedPath = string.Concat("/", currName, path);
                return urlHelper.Content(localizedPath);
            }

            if (path.StartsWith("~/", StringComparison.InvariantCultureIgnoreCase))
            {
                var localizedPath = string.Concat("~/", currName, path.Substring(1));
                return urlHelper.Content(localizedPath);
            }

            return urlHelper.Content(path);

        }

        public static string LocalizedLayout<T>(this UrlHelpers<T> urlHelper, string layout)
        {
            var currentCulture = urlHelper.RenderContext.Context.Culture;

            string pname = string.Empty;
            try
            {
                if (currentCulture.Parent != null)
                    pname = currentCulture.Parent.Name;
                if (pname.Equals("fr") || currentCulture.Name.Equals("fr"))
                {
                    string file = System.IO.Path.GetFileNameWithoutExtension(layout);
                    return file + "-fr.cshtml";

                }
            }
            catch 
            {
            }
            

            return layout;
        }

        //public static Response AsError(this IResponseFormatter formatter, IViewFactory factory, Exception exception, string title = null)
        //{
        //    var model = new ErrorResponseModel() {Description = exception.Message, Title = title};
        //    return AsError(formatter, factory, model);
        //}

        //public static Response AsError(this IResponseFormatter formatter, IViewFactory factory, ErrorResponseModel responseModel)
        //{
        //    var viewContext = new ViewLocationContext { Context = formatter.Context };
        //    var response = factory.RenderView("error_page", responseModel, viewContext);
        //    response.StatusCode = HttpStatusCode.InternalServerError;
        //    return response;
        //}

        public static Response AsRedirectLocalized(this IResponseFormatter formatter, string location, RedirectResponse.RedirectType type = RedirectResponse.RedirectType.SeeOther)
        {

            var currentCulture = formatter.Context.Culture;
            if (string.IsNullOrWhiteSpace(location) || currentCulture == null)
                return formatter.AsRedirect(location, type);

            if (currentCulture.Name.StartsWith("en") || currentCulture.Name.Equals("en"))
                return formatter.AsRedirect(location, type);

            string currName = currentCulture.Name.Length > 2 ? currentCulture.Name.Substring(0, 2) : currentCulture.Name;

            if (location.StartsWith("/"))
            {
                var localizedPath = string.Concat("/", currName, location);
                return formatter.AsRedirect(localizedPath, type);
            }

            if (location.StartsWith("~/"))
            {
                var localizedPath = string.Concat("~/", currName, location.Substring(1));
                return formatter.AsRedirect(localizedPath, type);
            }

            return formatter.AsRedirect(location, type);
        }

    }

}