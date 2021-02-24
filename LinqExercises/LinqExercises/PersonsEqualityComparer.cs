using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LinqExercises
{
    public class PersonsEqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals([AllowNull] Person x, [AllowNull] Person y)
        {
            if ((x is null) && (y is null))
            {
                return true;
            }

            if ((x is null) || (y is null))
            {
                return false;
            }

            bool areEqual = string.Equals(x.FirstName, y.FirstName) &&
                            string.Equals(x.LastName, y.LastName) &&
                            (x.DateOfBirth == y.DateOfBirth) &&
                            (x.Gender == y.Gender);

            return areEqual;
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            return HashCode.Combine(obj.FirstName, obj.LastName, obj.DateOfBirth, obj.Gender);
        }
    }
}
