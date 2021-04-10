using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Models;

namespace Payroll.Tests.Models
{
    /// <summary>
    ///     Class for testing employee's logic.
    /// </summary>
    [TestClass]
    public class EmployeeTest
    {
        /// <summary>
        ///     Method for testing salary of one employee on transmitted date.
        /// </summary>
        /// <param name="yearsOfWork">Calculate salary in transmitted years of work.</param>
        /// <param name="expect">Expected value of salary.</param>
        [DataTestMethod]
        [DataRow(0, 100)] // Test base salary
        [DataRow(5, 115)] // Test year bonus
        [DataRow(40, 130)] // Test max year bonus
        [DataRow(-10, 100)] // Wrong date(is set to 0 years)
        public void TestGetEmployeeFullSalaryFor0Years(int yearsOfWork, double expect)
        {
            // Arrange
            var employee = new Employee(DateTime.Now, 100);

            // Act
            var actual = employee.GetFullSalary(DateTime.Now.AddYears(yearsOfWork));

            // Assert
            Assert.AreEqual(expect, actual);
        }
    }
}