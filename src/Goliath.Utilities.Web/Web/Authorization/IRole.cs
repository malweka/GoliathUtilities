namespace Goliath.Web.Authorization
{
    public interface IRole : Microsoft.AspNet.Identity.IRole<long>
    {
 
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        int RoleNumber { get; }

    }
}