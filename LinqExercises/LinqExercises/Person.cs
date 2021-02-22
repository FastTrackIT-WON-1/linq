using System;

namespace LinqExercises
{
    public class Person
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
    }
}
