using System;
using DemoApplication;
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
        [DataRow(-3, 1206.0204390224999)] // Test base salary.
        [DataRow(5, 1449.0801621465498)] // Test year bonus.
        [DataRow(40, 1628.0221714792501)] // Test max year bonus.
        [DataRow(-10, 1206.0204390224999)] // Wrong date(is set to 0 years).
        public void CompanySalaryCalculatorVisitorTestCalculation(int yearsOfWork, double expected)
        {
            // Arrange
            var demo = new Demo();
            var employeeRepository = demo.InitEmployeeRepository();

            // Act
            var actual = employeeRepository.GetSumOfEmployesSalariesForDate(DateTime.Now.AddYears(yearsOfWork));

            // Assert
            Assert.AreEqual(expected, actual);
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
            employeeRepository.AddEmployee(employee);
            employeeRepository.AddEmployee(manager);
            manager.AddSubordinate(employee);
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
            employeeRepository.AddEmployee(employee);
            employeeRepository.AddEmployee(manager);
            manager2.AddSubordinate(manager);
            manager.AddSubordinate(employee);
            employeeRepository.RemoveEmployee(manager);

            // Assert
            Assert.IsTrue(manager2.HasSubordinate(employee));
            Assert.IsTrue(employee.HasHead());
        }
    }
}