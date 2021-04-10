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