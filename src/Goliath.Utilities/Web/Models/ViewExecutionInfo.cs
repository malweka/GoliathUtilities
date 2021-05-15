using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Goliath.Models
{
    [Serializable]
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
        [XmlIgnore]
        public Dictionary<string, ErrorInfo> Errors
        {
            get { return errors; }
        }
    }
}