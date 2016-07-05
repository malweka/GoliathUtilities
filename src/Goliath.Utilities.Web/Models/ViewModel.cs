using System;
using System.Dynamic;
using System.Xml.Serialization;
using Goliath.Web;

namespace Goliath.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the tab.
        /// </summary>
        /// <value>
        /// The name of the tab.
        /// </value>
        public string TabName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the action.
        /// </value>
        public ViewActionType ActionType { get; set; }

        /// <summary>
        /// Gets or sets the execution information.
        /// </summary>
        /// <value>
        /// The execution information.
        /// </value>
        public ViewExecutionInfo ExecutionInfo { get; set; }



        /// <summary>
        /// Gets or sets the view model bag.
        /// </summary>
        /// <value>
        /// The view model bag.
        /// </value>
        [XmlIgnore]
        public dynamic ViewModelBag { get; set; }

        protected ViewModel()
        {
            ExecutionInfo = new ViewExecutionInfo();
            Id = -1;
            ViewModelBag = new ExpandoObject();
        }
    }
}