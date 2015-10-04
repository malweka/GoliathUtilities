namespace Goliath.Web
{
    public interface IRole
    {
        /// <summary>
        /// Gets or sets the role number.
        /// </summary>
        /// <value>
        /// The role number.
        /// </value>
        int RoleNumber { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }
        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        string AppName { get; set; }
    }
}