namespace Goliath.Web
{
    /// <summary>
    /// 
    /// </summary>
    public struct LoginToken
    {
        /// <summary>
        /// The user name
        /// </summary>
        public string UserName;

        /// <summary>
        /// The signature
        /// </summary>
        public string Signature;

        /// <summary>
        /// The resource
        /// </summary>
        public string Resource;

        /// <summary>
        /// The user agent
        /// </summary>
        public string UserAgent;
    }
}