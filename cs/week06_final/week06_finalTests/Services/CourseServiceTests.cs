using Microsoft.VisualStudio.TestTools.UnitTesting;
using week06_final.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using week06_final.Abstraction.Repository;
using week06_final.Abstraction.Services;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;
using week06_final.Models;
using week06_final.Models.Person;

namespace week06_final.Services.Tests
{
    [TestClass()]
    public class CourseServiceTests
    {
        private Mock<ILoggerWrapper<CourseService>> _mockLogger;
        private Mock<ICourseRepository> _mockCourseRepository;
        private Mock<IPaymentService> _mockPaymentService;
        private Mock<INotificationService> _mockNotificationService;
        private CourseService _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILoggerWrapper<CourseService>>();
            _mockCourseRepository = new Mock<ICourseRepository>();
            _mockPaymentService = new Mock<IPaymentService>();
            _mockNotificationService = new Mock<INotificationService>();
            _sut = new CourseService(_mockLogger.Object, _mockCourseRepository.Object, _mockPaymentService.Object, _mockNotificationService.Object);
        }

        [TestMethod]
        public async Task GetCourse_ShouldReturnCourse_WhenCourseNameIsValid()
        {
            // Arrange
            var course = new Course("Test Course", new DateTime(2024, 01, 01), 30, 999);
            _mockCourseRepository.Setup(repo => repo.GetCourseByNameAsync(It.IsAny<string>())).ReturnsAsync(course);

            // Act
            var result = await _sut.GetCourse(course.CourseName);

            // Assert
            Assert.AreEqual(course, result);
            _mockCourseRepository.Verify(repo => repo.GetCourseByNameAsync(course.CourseName), Times.Once);
        }
        [TestMethod]
        public async Task GetCourse_ShouldThrowArgumentException_WhenCourseNameIsNull()
        {
            // Arrange
            string courseName = null;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.GetCourse(courseName));
        }
        [TestMethod]
        public async Task AddCourse_ShouldCallCourseRepositoryAddCourseAsync_WhenCourseIsValid()
        {
            // Arrange
            var course = new Course("Test Course", new DateTime(2024, 01, 01), 30, 999);
            _mockCourseRepository.Setup(repo => repo.AddCourseAsync(course)).Returns(Task.CompletedTask);

            // Act
            await _sut.AddCourse(course);

            // Assert
            _mockCourseRepository.Verify(repo => repo.AddCourseAsync(course), Times.Once);
        }

        [TestMethod]
        public async Task AddCourse_ShouldThrowArgumentNullException_WhenCourseIsNull()
        {
            // Arrange
            Course course = null;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.AddCourse(course));
        }

        [TestMethod]
        public async Task GetCourses_ShouldReturnCourses_WhenCoursesExist()
        {
            // Arrange
            var courses = new List<Course>
            {
                new Course("Test Course 1", new DateTime(2024, 01, 01), 30, 999),
                new Course("Test Course 2", new DateTime(2024, 01, 01), 30, 999)
            };
            _mockCourseRepository.Setup(repo => repo.GetCoursesAsync()).ReturnsAsync(courses);

            // Act
            var result = await _sut.GetCourses();

            // Assert
            Assert.AreEqual(courses, result);
            _mockCourseRepository.Verify(repo => repo.GetCoursesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetCourses_ShouldNotSwallowException_WhenExceptionIsThrown()
        {
            // Arrange
            Exception exception = new Exception("Test Exception");
            _mockCourseRepository.Setup(repo => repo.GetCoursesAsync()).ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _sut.GetCourses());
        }

        [TestMethod]
        public async Task AddStudentToCourse_ShouldCallDependencies_WhenStudentAndCourseNameAreValid()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var courseName = "Test Course";
            _mockCourseRepository.Setup(repo => repo.AddStudentToCourseAsync(student, courseName)).Returns(Task.CompletedTask);
            _mockCourseRepository.Setup(repo => repo.GetCourseByNameAsync(It.IsAny<string>())).ReturnsAsync(new Course(courseName, new DateTime(2024, 01, 01), 30, 999));
            _mockPaymentService.Setup(service => service.GetPaymentStatus(student, courseName)).ReturnsAsync(true);

            // Act
            await _sut.AddStudentToCourse(student, courseName);

            // Assert
            _mockCourseRepository.Verify(repo => repo.AddStudentToCourseAsync(student, courseName), Times.Once);
            _mockPaymentService.Verify(paymentService => paymentService.GetPaymentStatus(student, courseName), Times.Once);
        }

        [TestMethod]
        public async Task AddStudentToCourse_ShouldThrowArgumentNullException_WhenStudentIsNull()
        {
            // Arrange
            Student student = null;
            var courseName = "Test Course";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.AddStudentToCourse(student, courseName));
        }

        [TestMethod]
        public async Task AddStudentToCourse_ShouldThrowArgumentException_WhenCourseNameIsNullOrWhiteSpace()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string courseName = null;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.AddStudentToCourse(student, courseName));
        }


        [TestMethod]
        public async Task AddStudentToCourse_ShouldThrowException_WhenCourseRepositoryThrowsException()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var courseName = "Test Course";
            var exception = new Exception("Test Exception");
            _mockCourseRepository.Setup(repo => repo.AddStudentToCourseAsync(student, courseName)).ThrowsAsync(exception);
            _mockCourseRepository.Setup(repo => repo.GetCourseByNameAsync(It.IsAny<string>())).ReturnsAsync(new Course(courseName, new DateTime(2024, 01, 01), 30, 999));
            _mockPaymentService
                .Setup(paymentservice => paymentservice.GetPaymentStatus(It.IsAny<Student>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            // Act & Assert
            Exception actualException = await Assert.ThrowsExceptionAsync<Exception>(() => _sut.AddStudentToCourse(student, courseName));
            Assert.AreEqual(exception, actualException);
        }

        [TestMethod]
        public async Task AddStudentToCourse_ShouldThrowException_WhenPaymentServiceThrowsException()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var courseName = "Test Course";
            var exception = new Exception("Test Exception");
            _mockCourseRepository.Setup(repo => repo.GetCourseByNameAsync(It.IsAny<string>())).ReturnsAsync(new Course(courseName, new DateTime(2024, 01, 01), 30, 999));
            _mockPaymentService
                .Setup(paymentservice => paymentservice.GetPaymentStatus(It.IsAny<Student>(), It.IsAny<string>()))
                .ThrowsAsync(exception);
            // Act & Assert
            Exception actualException = await Assert.ThrowsExceptionAsync<Exception>(() => _sut.AddStudentToCourse(student, courseName));
            Assert.AreEqual(exception, actualException);
        }
        [TestMethod]
        public async Task AddStudentToCourse_ShouldThrowException_WhenPaymentServiceNotConfirmedThePayment()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var courseName = "Test Course";
            _mockCourseRepository.Setup(repo => repo.GetCourseByNameAsync(It.IsAny<string>())).ReturnsAsync(new Course(courseName, new DateTime(2024, 01, 01), 30, 999));
            _mockPaymentService
                .Setup(paymentservice => paymentservice.GetPaymentStatus(It.IsAny<Student>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            
            var expectedExceptionMessage = "Course is not yet paid by student.";

            // Act & Assert
            Exception actualException = await Assert.ThrowsExceptionAsync<NotFoundException>(() => _sut.AddStudentToCourse(student, courseName));
            Assert.AreEqual(actualException.Message, expectedExceptionMessage);
        }
        [TestMethod]
        public async Task AddStudentToCourse_ShouldThrowException_WhenCourseNotFound()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var courseName = "Test Course";
            _mockCourseRepository.Setup(repo => repo.GetCourseByNameAsync(It.IsAny<string>()))!.ReturnsAsync((Course)null!);
           

            var expectedExceptionMessage = "Course not found.";

            // Act & Assert
            Exception actualException = await Assert.ThrowsExceptionAsync<NotFoundException>(() => _sut.AddStudentToCourse(student, courseName));
            Assert.AreEqual(actualException.Message, expectedExceptionMessage);
        }

        [TestMethod()]
        public void GetCourseStatisticsTest()
        {
           //Todo Write your test here
        }
    }
}