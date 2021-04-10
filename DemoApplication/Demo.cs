using System;
using Payroll.Core;
using Payroll.Models;

namespace DemoApplication
{
    /// <summary>
    ///     Class for demonstrating basic logic.
    /// </summary>
    public class Demo
    {
        /// <summary>
        ///     Method for initializing repository with employees
        /// </summary>
        /// <returns>Repository with employees.</returns>
        public EmployeeRepository InitEmployeeRepository()
        {
            // Arrange// Creating employees with date of employment and base salary without any bonuses.
            var sales = new Sales(DateTime.Now.AddYears(-11), 10000);
            var manager1 = new Manager(DateTime.Now.AddYears(-10), 8500);
            var employee1 = new Employee(DateTime.Now.AddYears(-8), 7300);
            var sales1 = new Sales(DateTime.Now.AddYears(-5), 6000);
            var employee2 = new Employee(DateTime.Now.AddYears(-2), 4000);
            var manager2 = new Manager(DateTime.Now.AddYears(-2), 4000);
            var manager3 = new Manager(DateTime.Now.AddYears(-1), 3700);
            var employee3 = new Employee(DateTime.Now.AddYears(-2), 4000);
            var employee4 = new Employee(DateTime.Now.AddYears(-3), 4500);
            var manager4 = new Manager(DateTime.Now.AddYears(-13), 9500);
            var manager5 = new Manager(DateTime.Now.AddYears(-7), 7300);
            var employee5 = new Employee(DateTime.Now.AddYears(-2), 3000);
            var employee6 = new Employee(DateTime.Now.AddYears(-1), 2000);
            var manager6 = new Manager(DateTime.Now, 1800);

            // Linking all employees.
            sales.AddSubordinates(manager1, employee1, sales1);
            manager1.AddSubordinates(employee2, manager2);
            sales1.AddSubordinates(manager3);
            manager3.AddSubordinates(employee3);
            manager3.AddSubordinates(employee4);
            manager4.AddSubordinates(manager5, employee5);
            manager5.AddSubordinates(employee6, manager6);

            // Creating employee repository.
            var repository = new EmployeeRepository();

            // Linking employee repository.
            repository.AddEmployees(sales, sales1, manager1, manager2, manager3, manager4, manager5, manager6, employee1, employee2, employee3, employee4, employee5, employee6);

            return repository;
        }
    }
}