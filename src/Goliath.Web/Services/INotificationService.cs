namespace Goliath.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Send(NotificationMessage message);
    }
}
