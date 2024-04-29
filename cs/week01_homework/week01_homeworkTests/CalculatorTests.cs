using Microsoft.VisualStudio.TestTools.UnitTesting;
using week01_homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week001_homework;

namespace week01_homework.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [TestCategory(nameof(Calculator.Add))]
        [DataTestMethod()]
        [DataRow(1, 2, 3)]
        [DataRow(-1, -2, -3)]
        [DataRow(0, 0, 0)]
        [DataRow(100, 200, 300)]
        [DataRow(-100, 200, 100)]
        [DataRow(6.88, 2.55, 9.43)]
        [DataRow(62, 12, 74)]
        [DataRow(12, 62, 74)]
        public void GivenTwoDecimals_WhenAdd_ThenCorrectSumReturned(double a, double b, double expected)
        {
            // Act
            double actual = _calculator.Add(a, b);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCategory(nameof(Calculator.Subtract))]
        [DataTestMethod]
        [DataRow(5.0, 3.0, 2.0)]
        [DataRow(-5.0, -3.0, -2.0)]
        [DataRow(5.0, -3.0, 8.0)]
        [DataRow(-5.0, 3.0, -8.0)]
        [DataRow(0.0, 0.0, 0.0)]
        [DataRow(10, 2, 8)]
        [DataRow(2, 10, -8)]
        public void GivenTwoNumbers_WhenSubtracting_ThenCorrectDifferenceIsReturned(double a, double b, double expected)
        {
            // Act
            double actual = _calculator.Subtract(a, b);
            // Assert
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestCategory(nameof(Calculator.Multiply))]
        [DataTestMethod]
        [DataRow(1, 2, 2)]
        [DataRow(-1, -2, 2)]
        [DataRow(0, 5, 0)]
        [DataRow(5, 0, 0)]
        [DataRow(3.5, 2, 7)]
        [DataRow(-2.5, 4, -10)]
        [DataRow(62, 12, 744)]
        [DataRow(12, 62, 744)]
        public void GivenTwoNumbers_WhenMultiply_ThenCorrectProductIsReturned(double a, double b, double expected)
        {
            // Act
            double actual = _calculator.Multiply(a, b);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCategory(nameof(Calculator.Divide))]
        [DataTestMethod]
        [DataRow(10, 2, 5)]
        [DataRow(-10, -2, 5)]
        [DataRow(0, 1, 0)]
        [DataRow(10, -2, -5)]
        [DataRow(-10, 2, -5)]
        [DataRow(6.88, 2.55, 2.6980)]

        public void GivenValidNumbers_WhenDivide_ThenCorrectactual(double a, double b, double expected)
        {
            // Act
            var actual = _calculator.Divide(a, b);
            // Assert
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestCategory(nameof(Calculator.Divide))]
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void GivenZeroAsSecondParameter_WhenDivide_ThenDivideByZeroException()
        {
            //Arrange
            double a = 10;
            double b = 0;
            // Act
            _calculator.Divide(10, 0);
        }

        [TestCategory(nameof(Calculator.Sqrt))]
        [DataTestMethod]
        [DataRow(4, 2)]
        [DataRow(9, 3)]
        [DataRow(16, 4)]
        [DataRow(25, 5)]
        [DataRow(36, 6)]
        [DataRow(0, 0)]
        public void GivenPositiveNumber_WhenSqrtCalled_ThenCorrectRootReturned(double input, double expected)
        {
            // Act
            var actual = _calculator.Sqrt(input);
            // Assert
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestCategory(nameof(Calculator.Sqrt))]
        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-4)]
        [DataRow(-9)]
        [ExpectedException(typeof(Exception))]
        public void GivenNegativeNumber_WhenSqrtCalled_ThenExceptionThrown(double input)
        {
            // Act
            _calculator.Sqrt(input);
        }

        [TestCategory(nameof(Calculator.Power))]
        [DataTestMethod]
        [DataRow(2, 2, 4)]
        [DataRow(3, 3, 27)]
        [DataRow(4, 0.5, 2)]
        [DataRow(5, -1, 0.2)]
        [DataRow(0, 0, 1)]
        [DataRow(5, 0, 1)]
        [DataRow(0, 5, 0)]
        public void GivenTwoNumbers_WhenPowerIsCalled_ThenCorrectactualIsReturned(double a, double b, double expected)
        {
            // Act
            var actual = _calculator.Power(a, b);
            // Assert
            Assert.AreEqual(expected, actual, 0.0001);
        }
    }
}