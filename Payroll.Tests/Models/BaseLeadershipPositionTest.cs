using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Models;

namespace Payroll.Tests.Models
{
    /// <summary>
    ///     Class for testing logic of employees on leadership positions.
    /// </summary>
    [TestClass]
    public class BaseLeadershipPositionTest
    {
        /// <summary>
        ///     Method for testing more then one subordinate.
        /// </summary>
        [TestMethod]
        public void TestEmployeesAddTwoSubordinates()
        {
            // Arrange
            var employee = new Employee(DateTime.Now, 100);
            var employee2 = new Employee(DateTime.Now, 100);
            var manager = new Manager(DateTime.Now, 100);

            // Act
            manager.AddSubordinates(employee, employee2);

            // Assert
            Assert.IsTrue(manager.HasSubordinate(employee));
            Assert.IsTrue(manager.HasSubordinate(employee2));
        }

        /// <summary>
        ///     Method for testing exception firing after removing employee from subordinates. And this employee is not one of
        ///     them.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This employee is not Boss of selected one.")]
        public void TestEmployeesFailedToRemoveSubordinate()
        {
            // Arrange
            var employee = new Employee(DateTime.Now, 100);
            var manager = new Manager(DateTime.Now, 100);

            // Act
            manager.RemoveSubordinate(employee);
        }

        /// <summary>
        ///     Method for testing removing employee from subordinates.
        /// </summary>
        [TestMethod]
        public void TestEmployeesRemoveSubordinate()
        {
            // Arrange
            var employee = new Employee(DateTime.Now, 100);
            var manager = new Manager(DateTime.Now, 100);

            // Act
            manager.AddSubordinates(employee);
            manager.RemoveSubordinate(employee);

            // Assert
            Assert.IsFalse(manager.HasSubordinate(employee));
        }
    }
}