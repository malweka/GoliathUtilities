using System;
using System.Collections.Generic;

namespace Goliath.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ListViewModel<T> : ViewModel
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int TotalPages { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewModel{T}"/> class.
        /// </summary>
        public ListViewModel()
        {
            Items = new List<T>();
        }
    }
}