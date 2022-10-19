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
        long RoleNumber { get; }
        string Name { get; }
        bool IsAdminRole { get; }
    }
}