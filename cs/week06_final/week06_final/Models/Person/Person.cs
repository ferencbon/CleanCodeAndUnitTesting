using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week06_final.Models.Person
{
    public abstract class Person
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Email { get; set; }
      

        public Person(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string GetName()
        {
            return $"{FirstName} {LastName}";
        }
        public string GetEmail()
        {
            return Email;
        }
    }
}
