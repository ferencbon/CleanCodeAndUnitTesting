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

namespace week06_final.Clients.Tests
{
    [TestClass()]
    public class PushNotificationClientTests
    {
        private Mock<ILoggerWrapper<PushNotificationClient>> _mockLogger;
        private PushNotificationClient sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILoggerWrapper<PushNotificationClient>>();
            sut = new PushNotificationClient(_mockLogger.Object);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public async Task SendNotification_ShouldThrowNotificationException_WhenMessageIsNullOrEmpty(string message)
        {
            // Arrange & Act
            var actualException = await Assert.ThrowsExceptionAsync<NotificationException>(async () =>
            {
                await sut.SendNotificationAsync(message);
            });

            // Assert
            Assert.AreEqual("Message cannot be sent. See inner exception for details.", actualException.Message);
            Assert.IsInstanceOfType(actualException.InnerException, typeof(ArgumentNullException));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public async Task SendNotification_ShouldLoggerFunctionWasCalledWithRightException_WhenMessageIsNullOrEmpty(string message)
        {
            // Arrange & Act
            var actualException = await Assert.ThrowsExceptionAsync<NotificationException>(async () =>
            {
                await sut.SendNotificationAsync(message);
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
            await sut.SendNotificationAsync(message);

            // Assert
            _mockLogger.Verify(
                x => x.LogTrace(expectedTraceMessage),
                Times.Once);
        }
    }
}