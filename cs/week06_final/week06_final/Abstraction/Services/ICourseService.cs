using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Models;
using week06_final.Models.Person;

namespace week06_final.Abstraction.Services
{
    public interface ICourseService
    {
        Task<Course> GetCourse(string name);
        Task AddCourse(Course course);
        Task<List<Course>> GetCourses();
        Task AddStudentToCourse(Student student, string courseName);
        Task<CourseStatistic> GetCourseStatistics(string courseName);
    }
}
