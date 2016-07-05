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

        public int Offset;

        public int Limit;

        /// <summary>
        /// The sort string
        /// </summary>
        public string SortString;
    }
}