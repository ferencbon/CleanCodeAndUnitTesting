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
    public class GradeCalculatorTests
    {
        private GradeCalculator _gradeCalculator;

        [TestInitialize]
        public void Setup()
        {
            _gradeCalculator = new GradeCalculator();
        }

        [TestMethod()]
        [DataRow(100, "A")]
        [DataRow(99, "A")]
        [DataRow(90, "A")]
        [DataRow(89, "B")]
        [DataRow(80, "B")]
        [DataRow(79, "C")]
        [DataRow(70, "C")]
        [DataRow(69, "D")]
        [DataRow(25, "D")]
        [DataRow(1, "D")]
        [DataRow(0, "D")]
        public void CalculateGrade_ShouldReturnExpectedGrade_WhenCalledWithDifferentScores(int score, string expectedGrade)
        {
            string result = _gradeCalculator.CalculateGrade(score);
            Assert.AreEqual(expectedGrade, result);
        }
    }
}