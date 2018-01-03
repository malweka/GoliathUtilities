namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public static class UniqueIdFactory
    {
        /// <summary>
        /// Creates the identifier generator.
        /// </summary>
        /// <returns></returns>
        public static IUniqueNumberGenerator CreateIdGenerator()
        {
            //TODO: use configuration value for seed 
            return new UniqueLongGenerator();
        }
    }
}