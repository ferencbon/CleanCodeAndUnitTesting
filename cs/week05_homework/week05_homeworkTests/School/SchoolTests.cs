using Microsoft.VisualStudio.TestTools.UnitTesting;
using week05_homework.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Reflection;

namespace week05_homework.School.Tests
{
    [TestClass()]
    public class SchoolTests
    {
        private School _school;
        [TestInitialize]
        public void SetUp()
        {
            _school=new School();
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(50)]
        [DataRow(100)]
        public void AddClass_ShouldAddNewClass_UsingReflection(int countOfClasses)
        {
            // Arrange&Act
            for (int i = 0; i < countOfClasses; i++)
            {
                var schoolClass = new Mock<ISchoolClass>().Object;
                _school.AddClass(schoolClass);
            }
            

            // Assert
            var studentField = typeof(School).GetField("classes", BindingFlags.NonPublic | BindingFlags.Instance);
            var field = studentField.GetValue(_school) as List<ISchoolClass>;
            var count = field.Count;
            Assert.AreEqual(countOfClasses, field.Count);
        }
        

        [DataTestMethod]
        [DataRow(0,3,5)]
        [DataRow(1,21,55)]
        [DataRow(5,100,33)]
        public void GetStudentCount_ShouldReturnCorrectNumberOfStudents(int class1NumberOfStudents,int class2NumberOfStudents, int class3NumberOfStudents)
        {
            // Arrange
            var mockedSchoolClass1 = new Mock<ISchoolClass>();
            mockedSchoolClass1.Setup(c => c.GetStudentCount()).Returns(class1NumberOfStudents); 
            var mockedSchoolClass2 = new Mock<ISchoolClass>();
            mockedSchoolClass2.Setup(c => c.GetStudentCount()).Returns(class2NumberOfStudents);
            var mockedSchoolClass3 = new Mock<ISchoolClass>();
            mockedSchoolClass3.Setup(c => c.GetStudentCount()).Returns(class3NumberOfStudents);

            _school.AddClass(mockedSchoolClass1.Object);
            _school.AddClass(mockedSchoolClass2.Object);
            _school.AddClass(mockedSchoolClass3.Object);

            int expectedResult= class1NumberOfStudents+class2NumberOfStudents+class3NumberOfStudents;
            // Act
            var result = _school.GetStudentCount();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

      
    }
}