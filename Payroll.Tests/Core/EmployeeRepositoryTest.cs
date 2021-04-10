using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Core;
using Payroll.Models;

namespace Payroll.Tests.Core
{
    /// <summary>
    ///     Class for testing repository logic.
    /// </summary>
    [TestClass]
    public class EmployeeRepositoryTest
    {
        /// <summary>
        ///     Method for testing sum of salaries in one repository.
        /// </summary>
        /// <param name="yearsOfWork">Calculate sum of salaries in transmitted years of work.</param>
        /// <param name="expected">Expected value of salaries' sum.</param>
        [DataTestMethod]
        [DataRow(5, 97222.3889889)] // Test year bonus.
        [DataRow(40, 102984.46806725001)] // Test max year bonus.
        [DataRow(-10, 77441.1801825)] // Wrong date(is set to 0 years).
        public void CompanySalaryCalculatorVisitorTestCalculation(int yearsOfWork, double expected)
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

            // Act
            // Getting sum of all salaries in company.
            var actual = repository.GetSumOfEmployesSalariesForDate(DateTime.Now.AddYears(yearsOfWork));

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     Method for testing adding two employees.
        /// </summary>
        [TestMethod]
        public void TestRepositoryAddingTwoEmployees()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository();
            var employee = new Employee(DateTime.Now, 100);
            var employee2 = new Employee(DateTime.Now, 100);

            // Act
            employeeRepository.AddEmployees(employee, employee2);

            // Assert
            Assert.IsTrue(employeeRepository.ContainsEmployee(employee2));
            Assert.IsTrue(employeeRepository.ContainsEmployee(employee));
        }

        /// <summary>
        ///     Method for testing employee removing from repository.
        /// </summary>
        [TestMethod]
        public void TestRepositoryEmployeeRemoving()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository();
            var employee = new Employee(DateTime.Now, 100);
            var manager = new Manager(DateTime.Now, 100);

            // Act
            employeeRepository.AddEmployees(employee, manager);
            manager.AddSubordinates(employee);
            employeeRepository.RemoveEmployee(manager);

            // Assert
            Assert.IsFalse(employee.HasHead());
        }

        /// <summary>
        ///     Method for testing saving linking between two employees.
        ///     After third one between them was deleted.
        /// </summary>
        [TestMethod]
        public void TestRepositoryLinkingSavingBetweenTwoEmployeesAfterRemoving()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository();
            var employee = new Employee(DateTime.Now, 100);
            var manager = new Manager(DateTime.Now, 100);
            var manager2 = new Manager(DateTime.Now, 100);

            // Act
            employeeRepository.AddEmployees(employee, manager);
            manager2.AddSubordinates(manager);
            manager.AddSubordinates(employee);
            employeeRepository.RemoveEmployee(manager);

            // Assert
            Assert.IsTrue(manager2.HasSubordinate(employee));
            Assert.IsTrue(employee.HasHead());
        }
    }
}