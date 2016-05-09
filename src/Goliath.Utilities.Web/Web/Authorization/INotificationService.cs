using Goliath.Models;

namespace Goliath.Web.Authorization
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