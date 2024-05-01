using Microsoft.VisualStudio.TestTools.UnitTesting;
using week06_final.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;

namespace week06_final.Clients.Tests
{
    [TestClass()]
    public class EmailNotificationClientTests
    {
        private Mock<ILoggerWrapper<EmailNotificationClient>> _mockLogger;
        private EmailNotificationClient _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILoggerWrapper<EmailNotificationClient>>();
            _sut = new EmailNotificationClient(_mockLogger.Object);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public async Task SendNotification_ShouldThrowNotificationException_WhenMessageIsNullOrEmpty(string message)
        {
            // Arrange & Act
            var actualException = await Assert.ThrowsExceptionAsync<NotificationException>(async () =>
            {
                await _sut.SendNotificationAsync(message);
            });

            // Assert
            Assert.AreEqual("Message cannot be sent. See inner exception for details.", actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentException));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public async Task SendNotification_ShouldLoggerFunctionWasCalledWithRightException_WhenMessageIsNullOrEmpty(string message)
        {
            // Arrange & Act
            var actualException = await Assert.ThrowsExceptionAsync<NotificationException>(async () =>
            {
                await _sut.SendNotificationAsync(message);
            });

            var expectedException = actualException.InnerException;
            var expectedMessage = expectedException.Message;

            // Assert
            _mockLogger.Verify(
                x => x.LogError(expectedException, expectedMessage),
                Times.Once());
        }

        [TestMethod]
        public async Task SendNotification_ShouldLogTraceMessage_WhenMessageIsNotNullOrEmpty()
        {
            // Arrange
            var message = "Hello World!";
            var expectedTraceMessage = "Message was sent!";

            // Act
            await _sut.SendNotificationAsync(message);

            // Assert
            _mockLogger.Verify(
                x => x.LogTrace(expectedTraceMessage),
                Times.Once);
        }
    }
}