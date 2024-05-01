using Microsoft.VisualStudio.TestTools.UnitTesting;
using week06_final.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Abstraction.Clients;

namespace week06_final.Clients.Tests
{
    [TestClass()]
    public class DbClientTests
    {
        private IDbClient _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new DbClient();
        }

        [TestMethod]
        public async Task AddAsync_ShouldAddItem_WhenCalledWithValidKeyAndItem()
        {
            // Arrange
            string key = "testKey";
            string value = "testValue";

            // Act
            bool addResult = await _sut.AddAsync(key, value);

            // Assert
            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnItem_WhenCalledWithValidKey()
        {
            // Arrange
            string key = "testKey";
            string value = "testValue";
            await _sut.AddAsync(key, value);

            // Act
            string getResult = await _sut.GetAsync<string>(key);

            // Assert
            Assert.AreEqual(value, getResult);
        }

        [TestMethod]
        public async Task GetAsync_WhenKeyDoesNotExist_ReturnsDefault()
        {
            // Arrange
            string key = "nonexistentKey";

            // Act
            var result = await _sut.GetAsync<string>(key);

            // Assert
            Assert.AreEqual(default(string), result);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllItems_WhenCalled()
        {
            // Arrange
            string key1 = "testKey1";
            string value1 = "testValue1";
            string key2 = "testKey2";
            string value2 = "testValue2";
            await _sut.AddAsync(key1, value1);
            await _sut.AddAsync(key2, value2);

            // Act
            var getAllResult = await _sut.GetAllAsync<string>();

            // Assert
            CollectionAssert.Contains(getAllResult, value1);
            CollectionAssert.Contains(getAllResult, value2);
        }
        [TestMethod]
        public async Task AddAndGet_SameKeyDifferentTypes_ShouldReturnCorrectValues()
        {
            // Arrange
            string key = "testKey";
            int intValue = 123;
            string stringValue = "testValue";

            // Act
            await _sut.AddAsync(key, intValue);
            await _sut.AddAsync(key, stringValue);

            var retrievedIntValue = await _sut.GetAsync<int>(key);
            var retrievedStringValue = await _sut.GetAsync<string>(key);

            // Assert
            Assert.AreEqual(intValue, retrievedIntValue);
            Assert.AreEqual(stringValue, retrievedStringValue);
        }

        [TestMethod]
        public async Task AddAndGet_SameKeySameTypes_ShouldThrowException()
        {
            // Arrange
            string key = "testKey";
            string stringValue = "testValue";

            // Act
            await _sut.AddAsync(key, stringValue);


            // Assert
            Assert.ThrowsExceptionAsync<KeyExsistException>(()=> _sut.AddAsync(key, stringValue));
        }
    }
}