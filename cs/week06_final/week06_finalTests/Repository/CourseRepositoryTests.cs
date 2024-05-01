using Microsoft.VisualStudio.TestTools.UnitTesting;
using week06_final.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Moq;
using Snapshooter.MSTest;
using week06_final.Abstraction.Clients;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;
using week06_final.Models;
using week06_final.Abstraction.Repository;
using week06_final.Models.Person;

namespace week06_final.Repository.Tests
{
    [TestClass()]
    public class CourseRepositoryTests
    {
        private Mock<ILoggerWrapper<CourseRepository>> _mockLogger;
        private Mock<IDbClient> _mockDbClient;
        private CourseRepository _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILoggerWrapper<CourseRepository>>();
            _mockDbClient = new Mock<IDbClient>();
            _sut = new CourseRepository(_mockLogger.Object, _mockDbClient.Object);
        }

        [TestMethod]
        public async Task AddCourseAsync_ShouldCallDbClientAdd_WhenCourseIsValid()
        {
            // Arrange
            var course = new Course("Test Course", new DateTime(2024, 01, 01), 30, 2, 999);
            _mockDbClient.Setup(db => db.AddAsync(course.CourseName, course)).ReturnsAsync(true);

            // Act
            await _sut.AddCourseAsync(course);

            // Assert
            _mockDbClient.Verify(db => db.AddAsync(course.CourseName, course), Times.Once);
        }

        [TestMethod]
        public async Task AddCourseAsync_ShouldLogTraceInformation_WhenCourseIsValid()
        {
            // Arrange
            var course = new Course("Test Course", new DateTime(2024, 01, 01), 30, 2, 999);
            _mockDbClient.Setup(db => db.AddAsync(course.CourseName, course)).ReturnsAsync(true);

            // Act
            await _sut.AddCourseAsync(course);

            // Assert
            _mockLogger.Verify(logger => logger.LogTrace(It.Is<string>(s => s.Contains(course.CourseName))), Times.Once);

        }

        [TestMethod]
        public async Task AddCourseAsync_ShouldThrowExpectedException_WhenDbAddReturnFalse()
        {
            // Arrange
            var course = new Course("Test Course", new DateTime(2024, 01, 01), 30, 2, 999);
            _mockDbClient.Setup(db => db.AddAsync(course.CourseName, course)).ReturnsAsync(false);

            string ExpectedExceptionMessage = "Course cannot be added. See inner exception for details";
            string ExpectedInnerExceptionMessage = "Failed to add course to database.";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.AddCourseAsync(course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.AreEqual(ExpectedInnerExceptionMessage, actualException.InnerException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(Exception));
        }

        [TestMethod]
        public async Task AddCourseAsync_ShouldThrowExpectedException_WhenCurseIsNull()
        {
            // Arrange
            Course course = null;
            string ExpectedExceptionMessage = "Course cannot be added. See inner exception for details";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.AddCourseAsync(course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task AddCourseAsync_ShouldLogErrorCorrectly_WhenExceptionHappenedInDependency()
        {
            // Arrange
            var course = new Course("Test Course", new DateTime(2024, 01, 01), 30, 2, 999);
            var exception = new Exception("Test Exception");
            _mockDbClient.Setup(db => db.AddAsync(course.CourseName, course)).ThrowsAsync(exception);

            string ExpectedExceptionMessage = "Course cannot be added. See inner exception for details";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.AddCourseAsync(course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.AreEqual(exception, actualException.InnerException);
            _mockLogger.Verify(logger => logger.LogError(exception, exception.Message), Times.Once);
        }

        [TestMethod]
        public async Task AddCourseAsync_ShouldLogErrorCorrectly_WhenExceptionHappenedInternally()
        {
            // Arrange & Act
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.AddCourseAsync(null));

            string ExpectedExceptionMessage = "Course cannot be added. See inner exception for details";
            var expectedInnerException = actualException.InnerException;
            var expectedInnerMessage = actualException.InnerException.Message;

            // Assert
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            _mockLogger.Verify(logger => logger.LogError(expectedInnerException, expectedInnerMessage), Times.Once);
        }

        [TestMethod]
        public async Task AddStudentToCourseAsync_ShouldAddStudentToCourse_WhenStudentAndCourseAreValid()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = new Course("Course1", new DateTime(2024, 01, 01), 30, 2, 999);
            _mockDbClient.Setup(db => db.GetAsync<Course>(It.IsAny<string>())).ReturnsAsync(course);

            // Act
            await _sut.AddStudentToCourseAsync(student, course.CourseName);

            // Assert
            var isContainsStudent = course.GetStudents().Contains(student);
            Assert.IsTrue(isContainsStudent);
        }

        [TestMethod]
        public async Task AddStudentToCourseAsync_ShouldThrowArgumentNullException_WhenStudentIsNull()
        {
            // Arrange
            Student student = null;
            string courseName = "Course1";

            string ExpectedExceptionMessage = "Student cannot be added to course. See inner exception for details";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.AddStudentToCourseAsync(student, courseName));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task AddStudentToCourseAsync_ShouldThrowArgumentNullException_WhenCourseNameIsNull()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string courseName = null;

            string ExpectedExceptionMessage = "Student cannot be added to course. See inner exception for details";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.AddStudentToCourseAsync(student, courseName));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task AddStudentToCourseAsync_ShouldThrowNotFoundException_WhenCourseIsNotFound()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string courseName = "Course1";
            _mockDbClient.Setup(db => db.GetAsync<Course>(It.IsAny<string>())).ReturnsAsync((Course)null);
            string ExpectedExceptionMessage = "Student cannot be added to course. See inner exception for details";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.AddStudentToCourseAsync(student, courseName));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(NotFoundException));
        }

        [TestMethod]
        public async Task AddStudentToCourseAsync_ShouldLogErrorCorrectly_WhenExceptionHappenedInDependency()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string courseName = "Course1";
            var exception = new Exception("Test Exception");

            _mockDbClient.Setup(db => db.GetAsync<Course>(It.IsAny<string>())).ThrowsAsync(exception);

            string ExpectedExceptionMessage = "Student cannot be added to course. See inner exception for details";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.AddStudentToCourseAsync(student, courseName));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            _mockLogger.Verify(logger => logger.LogError(exception, exception.Message), Times.Once);
        }

        [TestMethod]
        public async Task AddStudentToCourseAsync_ShouldLogErrorCorrectly_WhenExceptionHappenedInternally()
        {
            // Arrange & Act
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.AddStudentToCourseAsync(null, null));

            string ExpectedExceptionMessage = "Student cannot be added to course. See inner exception for details";
            var expectedInnerException = actualException.InnerException;
            var expectedInnerMessage = actualException.InnerException.Message;

            // Assert
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            _mockLogger.Verify(logger => logger.LogError(expectedInnerException, expectedInnerMessage), Times.Once);
        }

        [TestMethod]
        public async Task AddStudentToCourseAsync_ShouldLogTraceInformation_WhenCourseIsValid()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = new Course("Course1", new DateTime(2024, 01, 01), 30, 2, 999);
            _mockDbClient.Setup(db => db.GetAsync<Course>(It.IsAny<string>())).ReturnsAsync(course);

            // Act
            await _sut.AddStudentToCourseAsync(student, course.CourseName);

            // Assert
            _mockLogger.Verify(logger => logger.LogTrace(It.Is<string>(s => s.Contains(course.CourseName))), Times.Once);
        }

        [TestMethod]
        public async Task GetCourseByNameAsync_ShouldReturnCourse_WhenCourseNameIsValid()
        {
            // Arrange
            var course = new Course("Course1", new DateTime(2024, 01, 01), 30, 2, 999);
            _mockDbClient.Setup(db => db.GetAsync<Course>(It.IsAny<string>())).ReturnsAsync(course);

            // Act
            var result = await _sut.GetCourseByNameAsync(course.CourseName);

            // Assert
            Assert.AreEqual(course, result);
            _mockDbClient.Verify(db => db.GetAsync<Course>(course.CourseName), Times.Once);
        }

        [TestMethod]
        public async Task GetCourseByNameAsync_ShouldThrowArgumentNullException_WhenCourseNameIsNull()
        {
            // Arrange
            string courseName = null;
            string ExpectedExceptionMessage = "Course cannot be retrieved. See inner exception for details";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.GetCourseByNameAsync(courseName));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task GetCourseByNameAsync_ShouldThrowArgumentNullException_WhenCourseNameIsEmpty()
        {
            // Arrange
            string courseName = string.Empty;
            string ExpectedExceptionMessage = "Course cannot be retrieved. See inner exception for details";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.GetCourseByNameAsync(courseName));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentException));
        }

        [TestMethod]
        public async Task GetCourseByNameAsync_ShouldReturnNull_WhenCourseIsNotFound()
        {
            // Arrange
            string courseName = "Course1";
            _mockDbClient.Setup(db => db.GetAsync<Course>(It.IsAny<string>())).ReturnsAsync((Course)null);

            // Act
            var actual = await _sut.GetCourseByNameAsync(courseName);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task GetCourseByNameAsync_ShouldLogErrorCorrectly_WhenExceptionHappenedInDependency()
        {
            // Arrange
            var courseName = "Course1";
            var exception = new Exception("Test Exception");

            _mockDbClient.Setup(db => db.GetAsync<Course>(It.IsAny<string>())).ThrowsAsync(exception);

            string ExpectedExceptionMessage = "Course cannot be retrieved. See inner exception for details";

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.GetCourseByNameAsync(courseName));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.AreEqual(exception, actualException.InnerException);
            _mockLogger.Verify(logger => logger.LogError(exception, exception.Message), Times.Once);
        }

        [TestMethod]
        public async Task GetCourseByNameAsync_ShouldLogErrorCorrectly_WhenExceptionHappenedInternally()
        {
            // Arrange & Act
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.GetCourseByNameAsync(null));

            string ExpectedExceptionMessage = "Course cannot be retrieved. See inner exception for details";
            var expectedInnerException = actualException.InnerException;
            var expectedInnerMessage = actualException.InnerException.Message;

            // Assert
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            _mockLogger.Verify(logger => logger.LogError(expectedInnerException, expectedInnerMessage), Times.Once);
        }

        [TestMethod]
        public async Task GetCoursesAsync_ShouldReturnCourses_WhenCoursesExist()
        {
            // Arrange
            var courses = new List<Course>
            {
                new Course("Course1", new DateTime(2024, 01, 01), 30, 2, 999),
                new Course("Course2", new DateTime(2024, 01, 01), 30, 2, 999)
            };
            _mockDbClient.Setup(db => db.GetAllAsync<Course>()).ReturnsAsync(courses);

            // Act
            var result = await _sut.GetCoursesAsync();

            // Assert
            Assert.AreEqual(courses, result);
        }

        [TestMethod]
        public async Task GetCoursesAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            Exception exception = new Exception("Test Exception");
            _mockDbClient.Setup(db => db.GetAllAsync<Course>()).ThrowsAsync(exception);

            // Act & Assert
            RepositoryException actualException = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.GetCoursesAsync());
            Assert.AreEqual(actualException.InnerException, exception);
            _mockLogger.Verify(logger => logger.LogError(exception, exception.Message), Times.Once);
        }

        [TestMethod]
        public async Task GetCourseStatistics_ShouldReturnCourseStatistics_WhenCourseExists()
        {
            // Arrange
            var courseName = "Test Course";
            var course = new Course (courseName, DateTime.Now.AddDays(-7), 10, 2,999);
            _mockDbClient.Setup(db => db.GetAsync<Course>(courseName)).ReturnsAsync(course);

            // Act
            var result = await _sut.GetCourseStatistics(courseName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(courseName, result.CourseName);
            _mockDbClient.Verify(db => db.GetAsync<Course>(courseName), Times.Once);
        }

        [TestMethod]
        public async Task GetCourseStatistics_ShouldThrowRepositoryException_WhenCourseDoesNotExist()
        {
            // Arrange
            var courseName = "Test Course";
            _mockDbClient.Setup(db => db.GetAsync<Course>(courseName)).ReturnsAsync((Course)null);

            // Act & Assert
           var actualException= await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.GetCourseStatistics(courseName));
           Assert.IsInstanceOfType<NotFoundException>(actualException.InnerException); 
           Assert.AreEqual(actualException.InnerException.Message,$"Course with name: {courseName} not found!");
           _mockDbClient.Verify(db => db.GetAsync<Course>(courseName), Times.Once);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task GetCourseStatistics_ShouldThrowExceptionWithEncapsulatedArgumentException_WhenCourseNameIsNullOrEmpty(string courseName)
        {
            // Act
            var exception = await Assert.ThrowsExceptionAsync<RepositoryException>(() => _sut.GetCourseStatistics(courseName));

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsNotNull(exception.InnerException);
            Assert.IsInstanceOfType(exception.InnerException, typeof(ArgumentException));
        }
        [TestMethod]
        public async Task GetCourseStatistics_ShouldReturnCorrectValues_WhenCourseExists()
        {
            // Arrange
            var courseName = "Test Course";
            var course = new Course(courseName, DateTime.Now.AddDays(-14),10, 2, 999);
            _mockDbClient.Setup(m => m.GetAsync<Course>(courseName)).ReturnsAsync(course);

            // Act
            var result = await _sut.GetCourseStatistics(courseName);

            Snapshot.Match(result);
           
        }
    }
}