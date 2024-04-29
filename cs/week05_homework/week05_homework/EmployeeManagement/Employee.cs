namespace week05_homework.EmployeeManagement;

public class Employee
{
    private string name;
    private double salary;

    public Employee(string name, double salary)
    {
        this.name = name;
        this.salary = salary;
    }

    public double CalculateSalary()
    {
        return salary;
    }
}