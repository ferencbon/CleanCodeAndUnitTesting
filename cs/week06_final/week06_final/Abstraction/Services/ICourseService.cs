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
        Task<Course> GetCourseAsync(string name);
        Task AddCourseAsync(Course course);
        Task<List<Course>> GetCoursesAsync();
        Task AddStudentToCourseAsync(Student student, string courseName);
        Task<CourseStatistic> GetCourseStatisticsAsync(string courseName);
    }
}
