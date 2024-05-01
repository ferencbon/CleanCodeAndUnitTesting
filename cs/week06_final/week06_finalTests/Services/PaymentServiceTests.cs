using Microsoft.VisualStudio.TestTools.UnitTesting;
using week06_final.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using week06_final.Abstraction.Clients;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;
using week06_final.Models.Person;
using week06_final.Models;

namespace week06_final.Services.Tests
{
    [TestClass()]
    public class PaymentServiceTests
    {

        private Mock<ILoggerWrapper<PaymentService>> _mockLogger;
        private Mock<IFinancialApiClient> _mockFinancialApiClient;
        private PaymentService _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILoggerWrapper<PaymentService>>();
            _mockFinancialApiClient = new Mock<IFinancialApiClient>();
            _sut = new PaymentService(_mockLogger.Object, _mockFinancialApiClient.Object);
        }

        [TestMethod]
        public async Task GetPaymentStatus_ShouldReturnTrue_WhenStudentAndCourseAreValid()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = "Course1";
            _mockFinancialApiClient.Setup(x => x.GetPaymentStatus(student, course)).ReturnsAsync(true);

            // Act
            var result = await _sut.GetPaymentStatusAsync(student, course);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetPaymentStatus_ShouldThrowException_WhenStudentIsNull()
        {
            // Arrange
            Student student = null;
            var course = "Course1";
            string ExpectedExceptionMessage = "Error in GetPaymentStatusAsync. See inner exception for details";

            // Act & Assert
            var actualExcepton = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.GetPaymentStatusAsync(student, course));
            Assert.AreEqual(ExpectedExceptionMessage, actualExcepton.Message);
            Assert.IsInstanceOfType(actualExcepton.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task GetPaymentStatus_ShouldThrowException_WhenCourseIsNull()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string course = null;
            string ExpectedExceptionMessage = "Error in GetPaymentStatusAsync. See inner exception for details";

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.GetPaymentStatusAsync(student, course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task GetPaymentStatus_ShouldThrowExceptionWithInnerException_WhenDependencyThrowsError()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = "Course1";
            var expectedInnerException = new Exception("Test Exception");
            _mockFinancialApiClient.Setup(x => x.GetPaymentStatus(student, course)).ThrowsAsync(expectedInnerException);

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.GetPaymentStatusAsync(student, course));
            Assert.AreEqual(actualException.InnerException, expectedInnerException);
        }

        [TestMethod]
        public async Task GetPaymentStatus_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = "Course1";

            var ExpectedInternalException = new Exception("Test Exception");
            _mockFinancialApiClient.Setup(x => x.GetPaymentStatus(student, course)).ThrowsAsync(ExpectedInternalException);

            // Act&Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.GetPaymentStatusAsync(student, course));

            _mockLogger.Verify(x => x.LogError(ExpectedInternalException, ExpectedInternalException.Message), Times.Once);

        }

        [TestMethod]
        public async Task CreatePayment_ShouldReturnTrue_WhenStudentAndCourseAreValid()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = "Course1";
            _mockFinancialApiClient.Setup(x => x.CreatePayment(student, course)).ReturnsAsync(true);

            // Act
            var result = await _sut.CreatePaymentAsync(student, course);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreatePayment_ShouldThrowException_WhenStudentIsNull()
        {
            // Arrange
            Student student = null;
            var course = "Course1";
            string ExpectedExceptionMessage = "Error in CreatePaymentAsync. See inner exception for details";

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.CreatePaymentAsync(student, course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task CreatePayment_ShouldThrowException_WhenCourseIsNull()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            string course = null;
            string ExpectedExceptionMessage = "Error in CreatePaymentAsync. See inner exception for details";

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.CreatePaymentAsync(student, course));
            Assert.AreEqual(ExpectedExceptionMessage, actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task CreatePayment_ShouldThrowExceptionWithInnerException_WhenDependencyThrowsError()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = "Course1";
            var expectedInnerException = new Exception("Test Exception");

            _mockFinancialApiClient.Setup(x => x.CreatePayment(student, course)).ThrowsAsync(expectedInnerException);

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.CreatePaymentAsync(student, course));
            Assert.AreEqual(actualException.InnerException, expectedInnerException);
        }

        [TestMethod]
        public async Task CreatePayment_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var student = new Student("firstName", "lastName", "email@email.com");
            var course = "Course1";

            var ExpectedInternalException = new Exception("Test Exception");
            _mockFinancialApiClient.Setup(x => x.CreatePayment(student, course)).ThrowsAsync(ExpectedInternalException);

            // Act&Assert
            var actualException = await Assert.ThrowsExceptionAsync<FinancialApiException>(() => _sut.CreatePaymentAsync(student, course));
            _mockLogger.Verify(x => x.LogError(ExpectedInternalException, ExpectedInternalException.Message), Times.Once);

        }
    }
}