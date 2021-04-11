using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Models;

namespace Payroll.Tests.Models
{
    /// <summary>
    ///     Class for testing employee logic.
    /// </summary>
    [TestClass]
    public class BaseEmployeeTest
    {
        /// <summary>
        ///     Method for testing exception firing after adding same employee to different chiefs.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This Employee already has Boss.")]
        public void TestEmployeesHeadAlreadyExist()
        {
            // Arrange
            var employee = new Employee(DateTime.Now, 100);
            var manager = new Manager(DateTime.Now, 100);
            var manager2 = new Manager(DateTime.Now, 100);

            // Act
            manager.AddSubordinates(employee);
            manager2.AddSubordinates(employee);
        }

        /// <summary>
        ///     Method for testing if after adding chief - employee will have chief.
        /// </summary>
        [TestMethod]
        public void TestEmployeesHeadIs()
        {
            // Arrange
            var employee = new Employee(DateTime.Now, 100);
            var manager = new Manager(DateTime.Now, 100);

            // Act
            manager.AddSubordinates(employee);

            // Assert
            Assert.IsTrue(manager.HasSubordinate(employee));
            Assert.IsTrue(employee.HeadIs(manager));
        }

        /// <summary>
        ///     Method for testing exception firing after adding same employee to different chiefs.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Employee is not set.")]
        public void TestEmployeesHeadIsNull()
        {
            // Arrange
            var employee = new Employee(DateTime.Now, 100);

            // Act
            employee.HeadIs(null);
        }

        /// <summary>
        ///     Method for testing if after removing chief - employee won't have chief.
        /// </summary>
        [TestMethod]
        public void TestEmployeesRemoveHead()
        {
            // Arrange
            var employee = new Employee(DateTime.Now, 100);
            var manager = new Manager(DateTime.Now, 100);

            // Act
            manager.AddSubordinates(employee);
            employee.RemoveHead();

            // Assert
            Assert.IsFalse(employee.HasHead());
        }
    }
}