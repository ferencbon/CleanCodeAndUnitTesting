namespace week05_homework.EmployeeManagement;

public class PayrollCalculator
{
    public PayrollCalculator()
    {
    }

    public double CalculatePayroll(List<Employee> employees)
    {
        // Simulation to calculate payroll for all employees
        double totalPayroll = 0;
        foreach (var employee in employees)
        {
            totalPayroll += employee.CalculateSalary();
        }
        return totalPayroll;
    }
}