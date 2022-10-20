using System;

namespace Goliath
{
    public class GoliathException : Exception
    {
        /// <summary>
        /// Gets or sets the error identifier.
        /// </summary>
        /// <value>
        /// The error identifier.
        /// </value>
        public string ErrorId { get; set; }

        /// <summary>
        /// Gets or sets the guest information.
        /// </summary>
        /// <value>
        /// The guest information.
        /// </value>
        public string GuestInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoliathException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GoliathException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoliathException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public GoliathException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SecurityException : GoliathException
    {
        public SecurityException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public SecurityException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }



    /// <summary>
    /// 
    /// </summary>
    public class GoliathEntityNotFoundException : GoliathException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LaventerDataException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GoliathEntityNotFoundException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaventerDataException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public GoliathEntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
