namespace Goliath.Authorization
{
    public interface IRole
    {
 
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        int RoleNumber { get; }
        string Name { get; }

    }
}