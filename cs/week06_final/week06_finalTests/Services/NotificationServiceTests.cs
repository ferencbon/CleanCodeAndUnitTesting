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

namespace week06_final.Services.Tests
{
    [TestClass()]
    public class NotificationServiceTests
    {
        private Mock<ILoggerWrapper<NotificationService>> _mockLogger;
        private List<Mock<INotificationClient>> _mockNotificationClients;
        private NotificationService _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILoggerWrapper<NotificationService>>();
            _mockNotificationClients = new List<Mock<INotificationClient>>
            {
                new Mock<INotificationClient>(),
                new Mock<INotificationClient>()
            };
            _sut = new NotificationService(_mockLogger.Object, _mockNotificationClients.Select(x => x.Object));
        }

        [TestMethod]
        public async Task SendNotification_ShouldCallSendNotificationAsync_OnAllClients()
        {
            // Arrange
            var message = "Test message";

            // Act
            await _sut.SendNotificationAsync(message);

            // Assert
            foreach (var mockClient in _mockNotificationClients)
            {
                mockClient.Verify(x => x.SendNotificationAsync(message), Times.Once);
            }
        }

        [TestMethod]
        public async Task SendNotification_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var message = "Test message";
            var exception = new Exception();
            _mockNotificationClients[0].Setup(x => x.SendNotificationAsync(message)).ThrowsAsync(exception);

            // Act
            var actualExceptionAsync = await Assert.ThrowsExceptionAsync<NotificationServiceException>(() => _sut.SendNotificationAsync(message));

            // Assert
            _mockLogger.Verify(x => x.LogError(exception, exception.Message), Times.Once);
        }

        [TestMethod]
        public async Task SendNotification_ShouldThrowNotificationServiceException_WhenExceptionIsThrown()
        {
            // Arrange
            var message = "Test message";
            var exception = new Exception();
            _mockNotificationClients[0].Setup(x => x.SendNotificationAsync(message)).ThrowsAsync(exception);

            // Act & Assert
            var actualException = await Assert.ThrowsExceptionAsync<NotificationServiceException>(() => _sut.SendNotificationAsync(message));
        }
    }
}