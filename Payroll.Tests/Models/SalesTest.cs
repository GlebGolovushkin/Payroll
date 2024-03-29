﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Models;

namespace Payroll.Tests.Models
{
    /// <summary>
    ///     Class for testing sales' logic.
    /// </summary>
    [TestClass]
    public class SalesTest
    {
        /// <summary>
        ///     Method for testing salary of one employee on transmitted date.
        /// </summary>
        /// <param name="yearsOfWork">Calculate salary in transmitted years of work.</param>
        /// <param name="expect">Expected value of salary.</param>
        [DataTestMethod]
        [DataRow(5, 105)] // Test year bonus.
        [DataRow(40, 135)] // Test max year bonus.
        [DataRow(-10, 100)] // Wrong date(is set to 0 years).
        public void TestGetSalesFullSalaryForNYears(int yearsOfWork, double expect)
        {
            // Arrange
            var sales = new Sales(DateTime.Now, 100);

            // Act
            var actual = sales.GetFullSalary(DateTime.Now.AddYears(yearsOfWork));

            // Assert
            Assert.AreEqual(expect, actual);
        }

        /// <summary>
        ///     Method for testing getting sales' salary if manager's subordinates don't have any subordinates.
        /// </summary>
        [TestMethod]
        public void TestSalesAddThreeSubordinatesOnOneLevelTest()
        {
            // Arrange
            const double expect = 100.9;
            var manager = new Manager(DateTime.Now, 100);
            var sales2 = new Sales(DateTime.Now, 100);
            var employee = new Employee(DateTime.Now, 100);
            var sales = new Sales(DateTime.Now, 100);

            // Act
            sales.AddSubordinates(sales2, employee, manager);
            var actual = sales.GetFullSalary(DateTime.Now);

            // Assert
            Assert.AreEqual(expect, actual);
        }

        /// <summary>
        ///     Method for testing getting sales' salary if manager's subordinates have subordinates.
        /// </summary>
        [TestMethod]
        public void TestSalesAddThreeSubordinatesOnTwoLevelTest()
        {
            // Arrange
            const double expect = 101;
            var manager = new Manager(DateTime.Now, 100);
            var sales2 = new Manager(DateTime.Now, 100);
            var employee = new Employee(DateTime.Now, 100);
            var employee2 = new Employee(DateTime.Now, 100);
            var employee3 = new Employee(DateTime.Now, 100);
            var employee4 = new Employee(DateTime.Now, 100);
            var employee5 = new Employee(DateTime.Now, 100);
            var sales = new Sales(DateTime.Now, 100);


            // Act
            sales.AddSubordinates(sales2, employee, manager);
            manager.AddSubordinates(employee2, employee3);
            sales2.AddSubordinates(employee4, employee5);
            var actual = manager.GetFullSalary(DateTime.Now);

            // Assert
            Assert.AreEqual(expect, actual);
        }
    }
}