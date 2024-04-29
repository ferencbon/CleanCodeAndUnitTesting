using Microsoft.VisualStudio.TestTools.UnitTesting;
using week04_homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week04_homework.Tests
{
    [TestClass()]
    public class UserDataProcessorTests
    {
        private UserDataProcessor _userDataProcessor;

        [TestInitialize]
        public void Setup()
        {
            _userDataProcessor = new UserDataProcessor();
        }

        [DataTestMethod]
        [DataRow(true, new string[] { "user1", "user2", "user3" }, true, "user2", 3, "User found: user2 at index 1")]
        [DataRow(true, new string[] { "user1", "user2", "user3" }, true, "user4", 3, "User not found.")]
        [DataRow(false, new string[] { }, true, "user2", 3, "Processing... Processing... Processing... ")]
        [DataRow(true, new string[] { }, false, "user2", 3, "No action taken.")]
        [DataRow(false, new string[] { }, false, "user2", 3, "No action taken.")]
        public void ProcessUserData_ShouldReturnExpectedResult_WhenCalledWithDifferentParameters(bool isSearchEnabled, string[] users, bool isProcessEnabled, string targetUserName, int processCount, string expectedResult)
        {
            string result = _userDataProcessor.ProcessUserData( isSearchEnabled, users, isProcessEnabled, targetUserName, processCount);
            Assert.AreEqual(expectedResult, result);
        }
    }
}