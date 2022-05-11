using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofdesQuiz2
{
    public class Employee
    {
        public Employee(string id, string firstName, string lastName, string profession)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Profession = profession;
        }

        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Profession { get; }
    }
}
