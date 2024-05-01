using Microsoft.VisualStudio.TestTools.UnitTesting;
using week06_final.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;
using week06_final.Models.Person;
using week06_final.Models;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace week06_final.Clients.Tests
{
    [TestClass()]
    public class FinancialApiClientTests
    {
        private Mock<ILoggerWrapper<FinancialApiClient>> _mockLogger;
        private FinancialApiClient _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILoggerWrapper<FinancialApiClient>>();
            _sut = new FinancialApiClient(_mockLogger.Object);
        }

        [TestMethod]
        public async Task CheckPaymentStatus_ShouldReturnTrue_WhenStudentAndCourseAreValid()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = "Course1";

            // Act
            var result = await _sut.GetPaymentStatus(student, course);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public async Task CheckPaymentStatus_ShouldThrowArgumentNullException_WhenStudentIsNull()
        {
            // Arrange
            Student student = null;
            var course = "Course1";
            string ExpectedExceptionMessage = "Error in GetPaymentStatus. See inner exception for details";

            // Act & Assert
            var actualException =await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.GetPaymentStatus(student, course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType<ArgumentNullException>(actualException.InnerException);
        }

        [TestMethod]
        public async Task CheckPaymentStatus_ShouldThrowArgumentNullException_WhenCourseIsNull()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string course = null;
            string ExpectedExceptionMessage = "Error in GetPaymentStatus. See inner exception for details";

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.GetPaymentStatus(student, course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType<ArgumentException>(actualException.InnerException);
        }

        [TestMethod]
        public async Task CheckPaymentStatus_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string course =null;

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.GetPaymentStatus(student, course));
            var expectedLoggedException = actualException .InnerException;
            var expectedLoggedExceptionMessage = actualException .InnerException.Message;
            _mockLogger.Verify(logger => logger.LogError(expectedLoggedException, expectedLoggedExceptionMessage), Times.Once);
        }

        [TestMethod]
        public async Task MakePayment_ShouldReturnTrue_WhenStudentAndCourseAreValid()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = "Course1";

            // Act
            var result = await _sut.CreatePayment(student, course);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task MakePayment_ShouldThrowArgumentNullException_WhenStudentIsNull()
        {
            // Arrange
            Student student = null;
            var course = "Course1";
            string ExpectedExceptionMessage = "Error in CreatePayment. See inner exception for details";

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.CreatePayment(student, course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType<ArgumentNullException>(actualException.InnerException);
        }

        [TestMethod]
        public async Task MakePayment_ShouldThrowArgumentNullException_WhenCourseIsNull()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string course = null;
            string ExpectedExceptionMessage = "Error in CreatePayment. See inner exception for details";

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.CreatePayment(student, course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType<ArgumentNullException>(actualException.InnerException);
        }

        [TestMethod]
        public async Task MakePayment_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string course = null;

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.CreatePayment(student, course));
            var expectedLoggedException = actualException.InnerException;
            var expectedLoggedExceptionMessage = actualException.InnerException.Message;
            _mockLogger.Verify(logger => logger.LogError(expectedLoggedException, expectedLoggedExceptionMessage), Times.Once);
        }
    }
}