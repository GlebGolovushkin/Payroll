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
            var employee = new Employee(DateTime.Now.AddYears(-3), 100);
            var employee1 = new Employee(DateTime.Now.AddYears(-2), 100);
            var employee2 = new Employee(DateTime.Now.AddYears(-1), 100);
            var employee3 = new Employee(DateTime.Now.AddYears(-2), 100);
            var sales = new Sales(DateTime.Now.AddYears(-1), 100);
            var sales1 = new Sales(DateTime.Now.AddYears(-2), 100);
            var sales2 = new Sales(DateTime.Now.AddYears(-3), 100);
            var sales3 = new Sales(DateTime.Now.AddYears(-2), 100);
            var manager = new Manager(DateTime.Now.AddYears(-3), 100);
            var manager1 = new Manager(DateTime.Now.AddYears(-2), 100);
            var manager2 = new Manager(DateTime.Now, 100);
            var manager3 = new Manager(DateTime.Now.AddYears(-1), 100);

            manager.AddSubordinate(sales);
            sales.AddSubordinate(manager1);
            manager1.AddSubordinate(employee);
            manager1.AddSubordinate(employee1);
            manager1.AddSubordinate(sales2);
            sales.AddSubordinate(manager2);
            manager2.AddSubordinate(sales1);
            sales1.AddSubordinate(employee2);
            sales3.AddSubordinate(manager3);
            manager3.AddSubordinate(employee3);

            var company = new EmployeeRepository();

            company.AddEmployee(sales);
            company.AddEmployee(sales1);
            company.AddEmployee(sales2);
            company.AddEmployee(sales3);
            company.AddEmployee(employee);
            company.AddEmployee(employee1);
            company.AddEmployee(employee2);
            company.AddEmployee(employee3);
            company.AddEmployee(manager);
            company.AddEmployee(manager1);
            company.AddEmployee(manager2);
            company.AddEmployee(manager3);

            return company;
        }
    }
}