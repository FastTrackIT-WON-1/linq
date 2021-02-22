using System.Collections.Generic;

namespace LinqExercises
{
    public static class NumbersGenerator
    {
        public static IEnumerable<int> Next()
        {
            int start = 0;
            while (true)
            {
                yield return start;
                start++;
            }
        }
    }
}
