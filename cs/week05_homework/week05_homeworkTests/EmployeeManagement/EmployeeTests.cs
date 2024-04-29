using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week05_homework.EmployeeManagement;

namespace week05_homeworkTests.EmployeeManagement
{
    [TestClass()]
    public class EmployeeTests
    {
        [TestMethod()]
        public void CalculateSalaryTest_ShouldReturnTheCorrectSalary()
        {
            //Arrange
            string name = "John";
            double salary = 1000;
            var employee = new Employee("John", 1000);
            //Act
            var result = employee.CalculateSalary();
            //Assert
            Assert.AreEqual(salary, result);
        }
    }
}