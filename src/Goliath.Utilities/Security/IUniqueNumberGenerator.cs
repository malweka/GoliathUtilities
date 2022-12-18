namespace Goliath.Security
{
    public interface IUniqueNumberGenerator
    {
        /// <summary>
        /// Gets the next id.
        /// </summary>
        /// <returns></returns>
        long GetNextId();

#if NET7_0_OR_GREATER
        static abstract IUniqueNumberGenerator Current{get;}
#endif
    }
}