namespace Goliath.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserAccessService
    {
        /// <summary>
        /// Validates the user credentials.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        UserSession ValidateUserCredentials(string userName, string password);

        /// <summary>
        /// Validates the social user.
        /// </summary>
        /// <param name="socialToken">The social token.</param>
        /// <returns></returns>
        UserSession ValidateSocialUser(string socialToken);

        /// <summary>
        /// Validates the token user.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="authenticationService">The authentication service.</param>
        /// <returns></returns>
        UserSession ValidateTokenUser(LoginToken token, IDeviceAuthenticationService authenticationService);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userid">The userid.</param>
        void ResetPassword(long userid);

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        bool ValidateToken(UserActivationModel token);

        ///// <summary>
        ///// Activates the user.
        ///// </summary>
        ///// <param name="token">The token.</param>
        ///// <returns></returns>
        //AppUser ActivateUser(UserActivationModel token);

        ///// <summary>
        ///// Creates the new user.
        ///// </summary>
        ///// <param name="user">The user.</param>
        //void CreateNewUser(AppUser user);

        /// <summary>
        /// Determines whether [is username exists] [the specified username].
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        bool IsUsernameExists(string username);
    }
}