using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week06_final.Models.Person
{
    public class Student : Person
    {
        public Student(string firstName, string lastName, string email) : base(
            firstName, lastName, email)
        {
        }
        private List<Course> RegisteredCourses = new List<Course>();
    }
}
