using System.Collections.Generic;

namespace Goliath.Models
{
    public class ViewExecutionInfo
    {
        /// <summary>
        /// Gets or sets the state of the execution.
        /// </summary>
        /// <value>
        /// The state of the execution.
        /// </value>
        public ViewExecutionState ExecutionState { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        readonly Dictionary<string, ErrorInfo> errors = new Dictionary<string, ErrorInfo>();

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public Dictionary<string, ErrorInfo> Errors
        {
            get { return errors; }
        }
    }
}