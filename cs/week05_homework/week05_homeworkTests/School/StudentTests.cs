using Microsoft.VisualStudio.TestTools.UnitTesting;
using week05_homework.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace week05_homework.School.Tests
{
    [TestClass()]
    public class StudentTests
    {
        [TestMethod]
        public void Constructor_ShouldSetStudentName_UsingReflection()
        {
            // Arrange
            string expectedName = "John Doe";
            var student = new Student(expectedName);

            // Act
            var nameField = typeof(Student).GetField("name", BindingFlags.NonPublic | BindingFlags.Instance);
            var actualName = nameField.GetValue(student) as string;

            // Assert
            Assert.AreEqual(expectedName, actualName);
        }
    }
}