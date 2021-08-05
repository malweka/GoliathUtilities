using System;
using System.Collections.Generic;

namespace Goliath.Security
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        const string alphabet = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ23456789*-+!@#$^&_~abcdefghijkmnopqrstuvwxyz";
        const string alphabetNoSpecialChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ23456789";
        private static readonly SecureRandom random = new SecureRandom();

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
        /// <param name="allowSpecialCharacters">if set to <c>true</c> [with special characters].</param>
        /// <returns></returns>
        public string Generate(int length, bool allowSpecialCharacters = false)
        {
            if (length < 2)
                throw new ArgumentException("length must be greater than 2", nameof(length));

            var stringCharacters = allowSpecialCharacters ? alphabet : alphabetNoSpecialChars;

            var value = new char[length];

            for (int i = 0; i < length; i++)
            {
                var index = random.Next(0, stringCharacters.Length);
                value[i] = stringCharacters[index];
            }

            return new string(value);
        }
    }
}