using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Abstraction.Repository;
using week06_final.Abstraction.Services;
using week06_final.Abstraction.Wrapper;
using week06_final.Models;
using week06_final.Models.Person;

namespace week06_final.Services
{
  

    public class CourseService : ICourseService
    {
        private readonly ILoggerWrapper<CourseService> _logger;
        private readonly ICourseRepository _courseRepository;
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;

        public CourseService(ILoggerWrapper<CourseService> _logger, ICourseRepository courseRepository, IPaymentService paymentService, INotificationService notificationService)
        {
            _logger = _logger;
            _courseRepository = courseRepository;
            _paymentService = paymentService;
            _notificationService = notificationService;
        }

        public async Task<Course> GetCourse(string name)
        {
            return await _courseRepository.GetCourseByNameAsync(name);
        }

        public async Task AddCourse(Course course)
        {
            await _courseRepository.AddCourseAsync(course);
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _courseRepository.GetCoursesAsync();
        }

        public async Task AddStudentToCourse(Student student, string courseName)
        {
            var course = await _courseRepository.GetCourseByNameAsync(courseName);
            if (course == null)
                throw new System.Exception("Course not found");

            var isCoursePaidByStudent = await _paymentService.GetPaymentStatus(student,course);
            if (!isCoursePaidByStudent)
            {
                throw new System.Exception("Course is not yet paid by student.");
            }

            await _courseRepository.AddStudentToCourseAsync(student, course.CourseName);
            await _notificationService.SendNotificationAsync($"{student.GetName()} was added to course.");
        }

        public async Task<CourseStatistic> GetCourseStatistics(string courseName)
        {
            return await _courseRepository.GetCourseStatistics(courseName);
        }
    }
}
