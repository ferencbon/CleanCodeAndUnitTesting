using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week06_final.Models.Person
{
    internal class Lecturer: Person
    {
        public Lecturer(string firstName, string lastName, string email) : base(
            firstName, lastName, email)
        {
        }

        private List<Course> AssignedCourses = new List<Course>();
    }
}
