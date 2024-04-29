using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week05_homework.EmployeeManagement
{
    public class EmployeeManagementSystem
    {
        private List<Employee> employees = new List<Employee>();

        public EmployeeManagementSystem()
        {
        }

        public void AddEmployee(Employee employee)
        {
            // Real-world code to add employee to the system
            employees.Add(employee);
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public void PromoteEmployee(Employee employee)
        {
            // Code to handle employee promotion logic
        }
    }
}
