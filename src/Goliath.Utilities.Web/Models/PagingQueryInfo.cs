namespace Goliath.Models
{
    public struct PagingQueryInfo
    {
        /// <summary>
        /// The total pages
        /// </summary>
        public int TotalPages;

        /// <summary>
        /// The current page
        /// </summary>
        public int CurrentPage;

        /// <summary>
        /// The filter
        /// </summary>
        public string Filter;

        /// <summary>
        /// The sort string
        /// </summary>
        public string SortString;
    }
}