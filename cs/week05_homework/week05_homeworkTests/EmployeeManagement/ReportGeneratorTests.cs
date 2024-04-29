using Microsoft.VisualStudio.TestTools.UnitTesting;
using week05_homework.EmployeeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week05_homework.EmployeeManagement.Tests
{
    [TestClass()]
    public class ReportGeneratorTests
    {
        [TestMethod]
        public void GenerateReports_ShouldReturnExpectedString()
        {
            // Arrange
            var sut= new ReportGenerator();

            // Act
            var report = sut.GenerateReports();
            // Assert
            Assert.AreEqual("Employee reports: ...", report);
        }
    }
}