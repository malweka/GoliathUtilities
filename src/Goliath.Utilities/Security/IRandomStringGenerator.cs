namespace Goliath.Security
{
    public interface IRandomStringGenerator
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns></returns>
        string Generate();

        /// <summary>
        /// Generates the specified length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="withSpecialCharacters">if set to <c>true</c> [with special characters].</param>
        /// <returns></returns>
        string Generate(int length, bool withSpecialCharacters = true);
    }
}