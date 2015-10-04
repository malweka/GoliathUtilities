namespace Goliath.Web.Services
{
    public interface IDeviceAuthenticationService
    {
        /// <summary>
        /// Verifies the client.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns></returns>
        bool VerifyClient(string clientName, string signature, long timestamp);
        /// <summary>
        /// Validates the user token.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="devicename">The devicename.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        bool ValidateUserToken(long userId, string devicename, string resourceName, string token);

        bool ValidateUserToken(IDevice device, string resourceName, string token);

        ///// <summary>
        ///// Registers the new device.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        ///// <param name="deviceName">Name of the device.</param>
        ///// <param name="deviceDescription">The device description.</param>
        ///// <returns></returns>
        //UserDevice RegisterNewDevice(long userId, string deviceName, string deviceDescription);
    }
}