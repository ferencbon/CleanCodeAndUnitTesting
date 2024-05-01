using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Models;
using week06_final.Models.Person;

namespace week06_final.Abstraction.Repository
{
    public interface ICourseRepository
    {
        Task AddCourseAsync(Course course);
        Task AddStudentToCourseAsync(Student student, string courseName);
        Task<Course> GetCourseByNameAsync(string courseName);
        Task<List<Course>> GetCoursesAsync();
        Task<CourseStatistic> GetCourseStatistics(string courseName);
    }
}
