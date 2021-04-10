using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Models;

namespace Payroll.Tests.Models
{
    /// <summary>
    ///     Class for testing manager's logic.
    /// </summary>
    [TestClass]
    public class ManagerTest
    {
        /// <summary>
        ///     Method for testing salary of one employee on transmitted date.
        /// </summary>
        /// <param name="yearsOfWork">Calculate salary in transmitted years of work.</param>
        /// <param name="expect">Expected value of salary.</param>
        [DataTestMethod]
        [DataRow(5, 125)] // Test year bonus.
        [DataRow(40, 140)] // Test max year bonus.
        [DataRow(-10, 100)] // Wrong date(is set to 0 years).
        public void TestGetManagerFullSalaryForNYears(int yearsOfWork, double expect)
        {
            // Arrange
            var employee = new Manager(DateTime.Now, 100);

            // Act
            var actual = employee.GetFullSalary(DateTime.Now.AddYears(yearsOfWork));

            // Assert
            Assert.AreEqual(expect, actual);
        }

        /// <summary>
        ///     Method for testing getting manager's salary if manager's subordinates don't have any subordinates.
        /// </summary>
        [TestMethod]
        public void TestManagerAddThreeSubordinatesOnOneLevelTest()
        {
            // Arrange
            var manager = new Manager(DateTime.Now, 100);
            var manager2 = new Manager(DateTime.Now, 100);
            var employee = new Employee(DateTime.Now, 100);
            var sales = new Sales(DateTime.Now, 100);

            // Act
            manager.AddSubordinates(manager2, employee, sales);
            var actual = manager.GetFullSalary(DateTime.Now);
            const double expect = 101.5;

            // Assert
            Assert.AreEqual(expect, actual);
        }

        /// <summary>
        ///     Method for testing getting manager's salary if manager's subordinates have subordinates.
        /// </summary>
        [TestMethod]
        public void TestManagerAddThreeSubordinatesOnTwoLevelTest()
        {
            // Arrange
            const double expect = 101.504;
            var manager = new Manager(DateTime.Now, 100);
            var manager2 = new Manager(DateTime.Now, 100);
            var employee = new Employee(DateTime.Now, 100);
            var employee2 = new Employee(DateTime.Now, 100);
            var employee3 = new Employee(DateTime.Now, 100);
            var sales = new Sales(DateTime.Now, 100);

            // Act
            manager.AddSubordinates(manager2, employee, sales);
            manager2.AddSubordinates(employee2);
            sales.AddSubordinates(employee3);
            var actual = manager.GetFullSalary(DateTime.Now);

            // Assert
            Assert.AreEqual(expect, actual);
        }
    }
}