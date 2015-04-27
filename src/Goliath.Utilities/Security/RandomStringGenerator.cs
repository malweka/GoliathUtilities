using System.Collections.Generic;

namespace Goliath.Security
{
    public class RandomStringGenerator
    {
        const string alphabet = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ23456789*-+!@#$^&_~abcdefghijkmnopqrstuvwxyz";
        private const string alphabetNoSpecialChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ23456789";

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
        /// <returns></returns>
        public string Generate(int length, bool withSpecialCharacters = true)
        {
            if (length < 5) length = 5;
            var rtxt = new List<char>();
            var random = new SecureRandom();

            string alpha;
            alpha = withSpecialCharacters ? alphabet : alphabetNoSpecialChars;
           
            for (int i = 0; i < length; i++)
            {
                var index = random.Next(0, alpha.Length);
                rtxt.Add(alpha[index]);
            }

            return new string(rtxt.ToArray());
        }
    }
}