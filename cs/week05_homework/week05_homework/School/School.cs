using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week05_homework.School
{
    public class School
    {
        private List<ISchoolClass> classes = new List<ISchoolClass>();

        public School() { }

        public void AddClass(ISchoolClass schoolClass)
        {
            classes.Add(schoolClass);
        }

        public int GetStudentCount()
        {
            int totalStudents = 0;
            foreach (var schoolClass in classes)
            {
                totalStudents += schoolClass.GetStudentCount();
            }
            return totalStudents;
        }
    }
}
