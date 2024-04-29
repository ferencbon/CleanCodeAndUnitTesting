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
    public class PayrollCalculatorTests
    {
        [TestMethod]
        public void CalculatePayroll_ShouldCalculateCorrectPayrollForMultipleEmployees()
        {
            // Arrange
            var sut= new PayrollCalculator();
            double salary1 = 50000;
            double salary2 = 60000;
            double expectedPayroll = 110000;
            var employee1 = new Employee("John Doe", salary1);
            var employee2 = new Employee("Jane Doe", salary2);
            

            List<Employee> employees = new List<Employee>();
            employees.Add(employee1);
            employees.Add(employee2);
            
            // Act
            var payroll = sut.CalculatePayroll(employees);
            
            // Assert
            Assert.AreEqual(expectedPayroll, payroll);
        }
    }
}