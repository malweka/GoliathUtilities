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
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public long Id{ get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name{ get; set; }
    }

	/// <summary>
	/// Dav property.
	/// </summary>
    public class DavProperty
    { 
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name{ get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value{ get; set; }      
    }

	public class DavResponse
	{
	}

	public class DavRequest
	{
	}
}
