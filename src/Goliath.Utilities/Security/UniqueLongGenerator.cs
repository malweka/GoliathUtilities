using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goliath.Security
{
    public class UniqueLongGenerator : IUniqueNumberGenerator
    {
        private const int Epoch = 1970;
        private readonly int seed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueLongGenerator"/> class.
        /// </summary>
        public UniqueLongGenerator() : this(2) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueLongGenerator"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// seed cannot be greater than 949
        /// or
        /// seed must be greater than or equal to zero
        /// </exception>
        public UniqueLongGenerator(int seed)
        {
            if (seed > 99)
                throw new ArgumentOutOfRangeException("seed cannot be greater than 99");

            if (seed < 0)
                throw new ArgumentOutOfRangeException("seed must be greater than or equal to zero");

            if (seed == 0)
                seed = 2;

            this.seed = seed;
        }


        #region IUniqueIdGenerator Members

        /// <summary>
        /// Gets the next id.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">seed cannot be greater than 256</exception>
        public long GetNextId()
        {
            var date = DateTime.UtcNow;
            var epoch = date.Year - Epoch;

            var rand = new SecureRandom();
            var randVal = rand.Next(1, 98);
            var seedVal = randVal / seed;

            var stringId = string.Concat(epoch.ToString("D2"), seedVal.ToString("D2"), date.Month.ToString("D2"), date.Day.ToString("D2"), date.Hour.ToString("D2"),
                date.Minute.ToString("D2"), date.Second.ToString("D2"), date.Millisecond.ToString("D3"), randVal.ToString("D2"));

            //let's wait at least 1 millisecond to avoid collisions
            System.Threading.Thread.Sleep(1);

            return Convert.ToInt64(stringId);

        }

        #endregion
    }


}
