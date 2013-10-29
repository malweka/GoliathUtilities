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
		string Name{ get;}

		/// <summary>
		/// Execute the specified request.
		/// </summary>
		/// <param name="request">Request.</param>
		DavResponse Execute (DavRequest request);
	}
}

