using Microsoft.VisualStudio.TestTools.UnitTesting;
using week06_final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Models.Person;
using System.Diagnostics;

namespace week06_final.Models.Tests
{
    [TestClass]
    public class CourseTests
    {
        [TestMethod]
        public void AddStudent_ShouldAddStudentToList()
        {
            // Arrange
            var course = new Course("Math", DateTime.Now, 10, 1000);
            var student = new Student("John", "Doe", "test@test.hu");

            // Act
            course.AddStudent(student);

            // Assert
            CollectionAssert.Contains(course.GetStudents(), student);
        }

        [TestMethod]
        public void GetStudents_ShouldReturnListOfStudents()
        {
            // Arrange
            var sut = new Course("Math", DateTime.Now, 10, 1000);
            var student1 = new Student("John", "Doe", "test1@test.hu");
            var student2 = new Student("Jane", "Smith", "test2@test.hu");
            sut.AddStudent(student1);
            sut.AddStudent(student2);

            // Act
            var students = sut.GetStudents();

            // Assert
            Assert.AreEqual(2, students.Count);
            CollectionAssert.Contains(students, student1);
            CollectionAssert.Contains(students, student2);
        }
    }
}