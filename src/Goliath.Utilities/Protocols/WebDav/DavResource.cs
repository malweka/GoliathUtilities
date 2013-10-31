using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goliath.Protocols.WebDav
{
    /// <summary>
    /// Dav resource.
    /// </summary>
    public abstract class DavResource
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        #region DAV Properties

        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Contains a description of the resource that is suitable for presentation to
        ///a user. This property is defined on the resource, and hence SHOULD
        ///have the same value independent of the Request-URI used to retrieve it
        ///(thus, computing this property based on the Request-URI is
        ///deprecated). While generic clients might display the property value to
        ///end users, client UI designers must understand that the method for
        ///identifying resources is still the URL. Changes to DAV:displayname do
        ///not issue moves or copies to the server, but simply change a piece of
        ///meta-data on the individual resource. Two resources can have the same
        ///DAV:displayname value even within the same collection.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// The DAV:getcontentlanguage property MUST be defined on any
        ///DAV-compliant resource that returns the Content-Language header on
        ///a GET.
        /// </summary>
        public string ContentLanguage { get; set; }

        /// <summary>
        /// Contains the Content-Length header returned by a GET without accept headers.
        /// The DAV:getcontentlength property MUST be defined on any
        /// DAV-compliant resource that returns the Content-Length header in
        /// response to a GET
        /// </summary>
        public int ContentLength { get; set; }

        /// <summary>
        /// Contains the Content-Type header value.
        /// This property MUST be defined on any DAV-compliant resource that returns the Content-Type header in response to a GET.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Contains the ETag header value as it would be returned by a GET without accept headers
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Contains the Last-Modified header value as it would be returned by a GET method without accept
        ///headers.
        /// </summary>
        public DateTime? LastModified { get; set; }

        public string ResourceType { get; protected set; }

        public LockEntry SupportedLockEntry { get; set; }

        #endregion
    }
}
