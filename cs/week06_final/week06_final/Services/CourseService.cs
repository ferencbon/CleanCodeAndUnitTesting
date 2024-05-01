using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Abstraction.Repository;
using week06_final.Abstraction.Services;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;
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

        public CourseService(ILoggerWrapper<CourseService> logger, ICourseRepository courseRepository, IPaymentService paymentService, INotificationService notificationService)
        {
            _logger = logger;
            _courseRepository = courseRepository;
            _paymentService = paymentService;
            _notificationService = notificationService;
        }

        public async Task<Course> GetCourseAsync(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            return await _courseRepository.GetCourseByNameAsync(name);
        }

        public async Task AddCourseAsync(Course course)
        {
            ArgumentNullException.ThrowIfNull(course);
            await _courseRepository.AddCourseAsync(course);
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            return await _courseRepository.GetCoursesAsync();
        }

        public async Task AddStudentToCourseAsync(Student student, string courseName)
        {
            CheckStudentAndCourseParameter(student, courseName);

            await ValidateCourseExistsAsync(courseName);

            await ValidateCoursePaidByStudentAsync(student, courseName);

            await _courseRepository.AddStudentToCourseAsync(student, courseName);
            await _notificationService.SendNotificationAsync($"{student.GetName()} was added to course.");
        }

        public async Task<CourseStatistic> GetCourseStatisticsAsync(string courseName)
        {
            return await _courseRepository.GetCourseStatistics(courseName, DateTime.Now);
        }

        private async Task ValidateCourseExistsAsync(string courseName)
        {
            var course = await _courseRepository.GetCourseByNameAsync(courseName);
            if (course == null)
                throw new NotFoundException("Course not found.");
        }

        private async Task ValidateCoursePaidByStudentAsync(Student student, string courseName)
        {
            var isCoursePaidByStudent = await _paymentService.GetPaymentStatusAsync(student, courseName);
            if (!isCoursePaidByStudent)
                throw new NotFoundException("Course is not yet paid by student.");
        }
        private void CheckStudentAndCourseParameter(Student student, string courseName)
        {
            ArgumentNullException.ThrowIfNull(student);
            ArgumentException.ThrowIfNullOrWhiteSpace(courseName);
        }
    }
}
