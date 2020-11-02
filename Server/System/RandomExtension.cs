using System.Collections.Generic;

namespace System
{
    public static class RandomExtension
    {
        public static int NextExclude(this Random random, IEnumerable<int> exclude)
        {
            var excludeHash = new HashSet<int>(exclude);
            var randomNumber = random.Next();

            while (excludeHash.Contains(randomNumber))
            {
                randomNumber = random.Next();
            }

            return randomNumber;
        }
    }
}