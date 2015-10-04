namespace Goliath.Web
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        /// <value>
        /// The name of the device.
        /// </value>
        string DeviceName { get; set; }
        /// <summary>
        /// Gets or sets the device description.
        /// </summary>
        /// <value>
        /// The device description.
        /// </value>
        string DeviceDescription { get; set; }
        /// <summary>
        /// Gets or sets the authentication pub key.
        /// </summary>
        /// <value>
        /// The authentication pub key.
        /// </value>
        string AuthPubKey { get; set; }
        /// <summary>
        /// Gets or sets the authentication priv key.
        /// </summary>
        /// <value>
        /// The authentication priv key.
        /// </value>
        string AuthPrivKey { get; set; }
        /// <summary>
        /// Gets or sets the device owner identifier.
        /// </summary>
        /// <value>
        /// The device owner identifier.
        /// </value>
        long? DeviceOwnerId { get; set; }
    }
}