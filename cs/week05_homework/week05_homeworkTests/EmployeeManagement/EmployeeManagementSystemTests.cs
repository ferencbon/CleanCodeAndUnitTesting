using Microsoft.VisualStudio.TestTools.UnitTesting;
using week05_homework.EmployeeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace week05_homework.EmployeeManagement.Tests
{
    [TestClass()]
    public class EmployeeManagementSystemTests
    {
       
        [TestMethod]
        public void AddEmployee_ShouldAddEmployeeToSystem()
        {
            var system = new EmployeeManagementSystem();
            var employee = new Mock<Employee>("Jhon",2000).Object;
            // Act
            system.AddEmployee(employee);

            // Assert
            List<Employee> employees = system.GetEmployees();
            bool result= employees.Contains((employee));
            Assert.IsTrue(result);
        }
    }
}