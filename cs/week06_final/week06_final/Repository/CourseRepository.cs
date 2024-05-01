using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Abstraction.Clients;
using week06_final.Abstraction.Repository;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;
using week06_final.Models;
using week06_final.Models.Person;


namespace week06_final.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ILoggerWrapper<CourseRepository> _logger;
        private readonly IDbClient _dbClient;

        public CourseRepository(ILoggerWrapper<CourseRepository> logger, IDbClient dbClient)
        {
            _logger = logger;
            _dbClient = dbClient;
        }

        public async Task AddCourseAsync(Course course)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(course);

                _logger.LogTrace($"Adding course with name: {course.CourseName} to database.");
                bool isAdded = await _dbClient.AddAsync(course.CourseName, course);

                if (!isAdded)
                    throw new Exception("Failed to add course to database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RepositoryException("Course cannot be added. See inner exception for details", ex);
            }
        }

        public async Task AddStudentToCourseAsync(Student student, string courseName)
        {
            try
            {
                ValidateStudentAndCourseName(student, courseName);

                var course = await GetCourseByNameAsync(courseName);
                if (course == null)
                    throw new NotFoundException($"Course with name: {courseName} not found!");

                _logger.LogTrace($"Adding student to course with name: {course.CourseName}.");
                course.AddStudent(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RepositoryException("Student cannot be added to course. See inner exception for details", ex);
            }
        }

        public async Task<Course> GetCourseByNameAsync(string courseName)
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(courseName);

                var course = await _dbClient.GetAsync<Course>(courseName);
                return course;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RepositoryException("Course cannot be retrieved. See inner exception for details", ex);
            }
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            try
            {
                var courses = await _dbClient.GetAllAsync<Course>();
                return courses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RepositoryException("Courses cannot be retrieved. See inner exception for details", ex);
            }
        }

        public async Task<CourseStatistic> GetCourseStatistics(string courseName, DateTime statisticDate)
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(courseName);
                ArgumentNullException.ThrowIfNull(statisticDate);

                var course = await GetCourseByNameAsync(courseName);
                ValidateCourseExists(course, courseName, statisticDate);

                _logger.LogTrace($"Calculating statistics for course with name: {course.CourseName}.");
                var courseStatistic = CalculateCourseStatistics(course, statisticDate);
                return courseStatistic;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new RepositoryException("Course statistics cannot be retrieved. See inner exception for details", ex);
            }
        }

        private void ValidateStudentAndCourseName(Student student, string courseName)
        {
            ArgumentNullException.ThrowIfNull(student);
            ArgumentException.ThrowIfNullOrWhiteSpace(courseName);
        }

        private void ValidateCourseExists(Course course, string courseName,DateTime statisticDate)
        {
            if (course == null)
                throw new NotFoundException($"Course with name: {courseName} not found!");
            if (course.StartDate > statisticDate)
                throw new Exception($"Course with name: {courseName} has not started yet at {statisticDate}!");
        }

        private CourseStatistic CalculateCourseStatistics(Course course, DateTime statisticDate)
        {
            var totalLectures = course.LengthInWeeks * course.LecturesEachWeek;
            var completedLectures = Convert.ToInt32(Math.Round((statisticDate - course.StartDate).TotalDays / 7) * course.LecturesEachWeek);
            var completionPercentage = ((double)completedLectures / (double)totalLectures);

            var courseStatistic = new CourseStatistic(
                course.CourseName,
                totalLectures,
                completedLectures,
                completionPercentage,
                course.StartDate
            );

            return courseStatistic;
        }
    }
}
