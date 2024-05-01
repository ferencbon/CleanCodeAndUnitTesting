using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Models.Person;

namespace week06_final.Models
{
    public class Course
    {
        private List<Student> _students = new List<Student>();
        public string CourseName { get; private set; }
        public DateTime StartDate { get; private set; }
        public int LengthInWeeks { get; private set; }
        public decimal CostInHuf { get; private set; }

        public Course(string courseName, DateTime startDate, int lengthInWeeks, int costInHuf)
        {
            CourseName = courseName;
            StartDate = startDate;
            LengthInWeeks = lengthInWeeks;
            CostInHuf = costInHuf;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public List<Student> GetStudents()
        {
            return _students;
        }
    }
}
