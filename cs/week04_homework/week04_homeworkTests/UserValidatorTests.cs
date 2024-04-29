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
    public class UserValidatorTests
    {
        private UserValidator _userValidator;

        [TestInitialize]
        public void Setup()
        {
            _userValidator = new UserValidator();
        }

        [TestMethod]
        [DataRow("validUser1", true, DisplayName = "ValidateUserInput_ShouldReturnTrue_WhenInputIsValid")]
        [DataRow("  validUser1  ", true, DisplayName = "ValidateUserInput_ShouldReturnTrue_WhenInputHasLeadingAndTrailingSpaces")]
        [DataRow("inva", false, DisplayName = "ValidateUserInput_ShouldReturnFalse_WhenInputIsLessThanFiveCharacters")]
        [DataRow("thisisaverylongusername", false, DisplayName = "ValidateUserInput_ShouldReturnFalse_WhenInputIsMoreThanTwentyCharacters")]
        [DataRow("invalidUser!", false, DisplayName = "ValidateUserInput_ShouldReturnFalse_WhenInputHasSpecialCharacters")]
        [DataRow("", false, DisplayName = "ValidateUserInput_ShouldReturnFalse_WhenInputIsEmpty")]
        public void ValidateUserInput_ShouldReturnExpectedResult_WhenCalledWithDifferentInputs(string input, bool expectedResult)
        {
            bool result = _userValidator.ValidateUserInput(input);
            Assert.AreEqual(expectedResult, result);
        }
    }
}