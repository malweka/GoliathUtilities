using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Goliath.Security
{
    public class UniqueLongGenerator : IUniqueNumberGenerator
    {
        private readonly int epoch;
        static readonly object lockpad = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueLongGenerator"/> class.
        /// </summary>
        public UniqueLongGenerator() : this(1970) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueLongGenerator"/> class.
        /// </summary>
        /// <param name="epoch">The seed.</param>
        public UniqueLongGenerator(int epoch)
        {
            if (epoch < 1900)
                epoch = 1970;

            this.epoch = epoch;
        }
        
        #region IUniqueIdGenerator Members

        /// <summary>
        /// Gets the next id.
        /// </summary>
        /// <returns></returns>
        public long GetNextId()
        {
            var date = DateTime.UtcNow;

            var secRand = new SecureRandom();
            lock (lockpad)
            {
                var randVal = secRand.Next(1, 998);
                var parts = new int[] { (date.Year - epoch), date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond };
                var valString = string.Concat(parts[0].ToString("D2"), parts[1].ToString("D2"), parts[2].ToString("D2"), parts[3].ToString("D2"),
                    parts[4].ToString("D2"), parts[5].ToString("D2"), parts[6].ToString("D3"), randVal.ToString("D3"));

                Thread.Sleep(5);
                return Convert.ToInt64(valString);
            }
        }

        #endregion
        
#if NET7_0_OR_GREATER
        static UniqueLongGenerator current = new UniqueLongGenerator();
        public static IUniqueNumberGenerator Current => current;
#endif
    }
}
