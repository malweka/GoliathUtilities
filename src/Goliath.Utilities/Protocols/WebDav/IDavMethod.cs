using System;

namespace Goliath.Protocols.WebDav
{
    /// <summary>
    /// WebDAV method
    /// </summary>
    public interface IDavMethod
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Execute the specified request.
        /// </summary>
        /// <param name="request">Request.</param>
        DavResponse Execute(DavRequest request);
    }

    class PropFindMethod : IDavMethod
    {
        private const string MethodName = "PROPFIND";

        #region IDavMethod Members

        public string Name
        {
            get { return MethodName; }
        }

        public DavResponse Execute(DavRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class OptionsMethod : IDavMethod
    {
        private const string MethodName = "OPTIONS";

        #region IDavMethod Members

        public string Name
        {
            get { return MethodName; }
        }

        public DavResponse Execute(DavRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

