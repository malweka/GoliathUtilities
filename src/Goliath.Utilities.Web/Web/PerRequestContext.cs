using System;
using Goliath.Data;
using Goliath.Web.Authorization;

namespace Goliath.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class PerRequestContext : ApplicationContext
    {
        private readonly IDatabaseProvider dbProvider;

        private readonly IPermissionService permissionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerRequestContext" /> class.
        /// </summary>
        /// <param name="dbProvider">The database provider.</param>
        /// <param name="permissionService">The permission service.</param>
        /// <param name="userSession">The user session.</param>
        /// <exception cref="System.ArgumentNullException">dbProvider</exception>
        public PerRequestContext(IDatabaseProvider dbProvider, IPermissionService permissionService, UserSession userSession)
        {
            if (dbProvider == null)
                throw new ArgumentNullException(nameof(dbProvider));

            this.dbProvider = dbProvider;
            this.permissionService = permissionService;
            this.CurrentUser = userSession;
        }

        ///// <summary>
        ///// Updates the identity.
        ///// </summary>
        ///// <param name="userSession">The user session.</param>
        //public void UpdateIdentity(UserSession userSession)
        //{
        //    this.userSession = userSession;
        //}

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public override UserSession CurrentUser { get; }

        /// <summary>
        /// Gets or sets the authorizaton validator.
        /// </summary>
        /// <value>
        /// The authorizaton validator.
        /// </value>
        public override PermissionValidator AuthorizatonValidator => new PermissionValidator(permissionService, CurrentUser);


        /// <summary>
        /// Gets or sets the session factory.
        /// </summary>
        /// <value>
        /// The session factory.
        /// </value>
        public override ISessionFactory SessionFactory => dbProvider.SessionFactory;
    }
}