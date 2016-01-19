using System.Collections.Generic;

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

    public class RandomStringGenerator : IRandomStringGenerator
    {
        const string alphabet = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ23456789*-+!@#$^&_~abcdefghijkmnopqrstuvwxyz";
        const string alphabetNoSpecialChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ23456789";

        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            return Generate(8);
        }

        /// <summary>
        /// Generates the specified length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="withSpecialCharacters">if set to <c>true</c> [with special characters].</param>
        /// <returns></returns>
        public string Generate(int length, bool withSpecialCharacters = true)
        {
            if (length < 5) length = 5;
            var rtxt = new List<char>();
            var random = new SecureRandom();

            var alpha = withSpecialCharacters ? alphabet : alphabetNoSpecialChars;
           
            for (var i = 0; i < length; i++)
            {
                var index = random.Next(0, alpha.Length);
                rtxt.Add(alpha[index]);
            }

            return new string(rtxt.ToArray());
        }
    }
}