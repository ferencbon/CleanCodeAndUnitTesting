using Microsoft.VisualStudio.TestTools.UnitTesting;
using week05_homework.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace week05_homework.School.Tests
{
    [TestClass()]
    public class SchoolClassTests
    {
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(10)]
        public void GetStudentCount_ShouldReturnCorrectStudentCount(int studentCount)
        {
            // Arrange
            var schoolClass = new SchoolClass();
            for (int i = 0; i < studentCount; i++)
            {
                schoolClass.AddStudent(new Student($"Student{i}"));
            }

            // Act
            var count = schoolClass.GetStudentCount();

            // Assert
            Assert.AreEqual(studentCount, count);
        }
    }
}