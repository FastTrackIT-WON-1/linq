using System;
using System.Diagnostics.CodeAnalysis;

namespace LinqExercises
{
    public class Person /*: IEquatable<Person>*/
    {
        public Person(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
        {
            this.FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            this.Gender = gender;
            this.DateOfBirth = dateOfBirth;
        }

        public string FirstName
        {
            get;
        }

        public string LastName
        {
            get;
        }

        public string FullName
            => $"{FirstName} {LastName}";

        public Gender Gender
        {
            get;
        }

        public DateTime DateOfBirth
        {
            get;
        }

        public int Age
            => DateTime.Today.Year - DateOfBirth.Year;

        /*
        public bool Equals([AllowNull] Person other)
        {
            if (other is null)
            {
                return false;
            }

            bool areEqual = string.Equals(this.FirstName, other.FirstName) &&
                            string.Equals(this.LastName, other.LastName) &&
                            (this.DateOfBirth == other.DateOfBirth) &&
                            (this.Gender == other.Gender);

            return areEqual;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Person);
        }

        */

        public void Print()
        {
            Print(-1);
        }

        public void Print(int index)
        {
            Console.WriteLine(
                index >= 0
                ? $"{index}) {FullName} date of birth: {DateOfBirth}, age: {Age}"
                : $"{FullName} date of birth: {DateOfBirth}, age: {Age}");
        }

        /*
        public override int GetHashCode()
        {
            HashCode hasher = new HashCode();
            if (!(this.FirstName is null))
            {
                hasher.Add(this.FirstName);
            }

            if (!(this.LastName is null))
            {
                hasher.Add(this.LastName ?? string.Empty);
            }

            hasher.Add(this.DateOfBirth);
            hasher.Add(this.Gender);

            return hasher.ToHashCode();
        }
        */
    }
}
