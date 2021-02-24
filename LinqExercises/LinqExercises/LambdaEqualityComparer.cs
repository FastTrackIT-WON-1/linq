using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LinqExercises
{
    public class LambdaEqualityComparer<T> : IEqualityComparer<T>
    {
        public LambdaEqualityComparer(
            Func<T, T, bool> equalityComparer,
            Func<T, int> hashCoder)
        {
            this.EqualityComparer = equalityComparer 
                ?? throw new ArgumentNullException(nameof(equalityComparer));

            this.HashCoder = hashCoder
                ?? throw new ArgumentNullException(nameof(hashCoder));
        }

        public Func<T, T, bool> EqualityComparer { get; }

        public Func<T, int> HashCoder { get; }

        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            if ((x is null) && (y is null))
            {
                return true;
            }

            if ((x is null) || (y is null))
            {
                return false;
            }

            return EqualityComparer(x, y);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return HashCoder(obj);
        }
    }
}
