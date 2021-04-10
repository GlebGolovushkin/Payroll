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
            // Creating employees with date of employment and base salary without any bonuses.
            var s = new Sales(DateTime.Now.AddYears(-11), 10000);
            var m1 = new Manager(DateTime.Now.AddYears(-10), 8500);
            var e1 = new Employee(DateTime.Now.AddYears(-8), 7300);
            var s1 = new Sales(DateTime.Now.AddYears(-5), 6000);
            var e2 = new Employee(DateTime.Now.AddYears(-2), 4000);
            var m2 = new Manager(DateTime.Now.AddYears(-2), 4000);
            var m3 = new Manager(DateTime.Now.AddYears(-1), 3700);
            var e3 = new Employee(DateTime.Now.AddYears(-2), 4000);
            var e4 = new Employee(DateTime.Now.AddYears(-3), 4500);
            var m4 = new Manager(DateTime.Now.AddYears(-13), 9500);
            var m5 = new Manager(DateTime.Now.AddYears(-7), 7300);
            var e5 = new Employee(DateTime.Now.AddYears(-2), 3000);
            var e6 = new Employee(DateTime.Now.AddYears(-1), 2000);
            var m6 = new Manager(DateTime.Now, 1800);

            // Linking all employees.
            s.AddSubordinates(m1, e1, s1);
            m1.AddSubordinates(e2, m2);
            s1.AddSubordinates(m3);
            m3.AddSubordinates(e3);
            m3.AddSubordinates(e4);
            m4.AddSubordinates(m5, e5);
            m5.AddSubordinates(e6, m6);

            // Creating employee repository.
            var repository = new EmployeeRepository();

            // Linking employee repository.
            repository.AddEmployees(s, s1, m1, m2, m3, m4, m5, m6, e1, e2, e3, e4, e5, e6);

            return repository;
        }
    }
}