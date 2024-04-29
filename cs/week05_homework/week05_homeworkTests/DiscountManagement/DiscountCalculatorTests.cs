using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week05_homework.DiscountManagement;

namespace week05_homeworkTests.DiscountManagement
{
    [TestClass()]
    public class DiscountCalculatorTests
    {
        private DiscountCalculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new DiscountCalculator();
        }

        [TestMethod]
        [DataRow("standard", 5)]
        [DataRow("silver", 10)]
        [DataRow("gold", 20)]
        [DataRow("unknown", 0)]
        public void CalculateDiscountPercentage_ShouldReturnCorrectPercentage(string level, int expectedDiscount)
        {
            // Arrange&Act
            var result = _calculator.CalculateDiscountPercentage(level);

            // Assert
            Assert.AreEqual(expectedDiscount, result);
        }

        [TestMethod]
        public void CalculateDiscountPercentage_ShouldThrowExceptionForNullInputArgument()
        {
            //Arrange
            string expectedErrorMessage = "level";
            try
            {
                //Act
                _calculator.CalculateDiscountPercentage(null);

                // Assert
                Assert.Fail("No exception was thrown.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(expectedErrorMessage, ex.ParamName);
            }
        }
    }
}