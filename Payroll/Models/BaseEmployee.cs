using System;
using Payroll.Models.Visitor;

namespace Payroll.Models
{
    /// <summary>
    ///     Class for one person with any type of employee.
    /// </summary>
    public abstract class BaseEmployee
    {
        // Base salary per month without any bonuses.
        private readonly double baseSalary;

        // Date of work start.
        private readonly DateTime startOfWork;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="startOfWork">Date on what employee started his work.</param>
        /// <param name="baseSalary">Base wage rate.</param>
        protected BaseEmployee(DateTime startOfWork, int baseSalary)
        {
            this.startOfWork = startOfWork;
            this.baseSalary = baseSalary;
        }

        // Employee's chief.
        internal BaseLeadershipPosition Head { get; set; }

        // Bonus for every year in company.
        protected abstract double yearBonusPercent { get; }

        // Max bonus for years in company.
        protected abstract double maxTimeBonus { get; }

        /// <summary>
        ///     Method to get only base salary plus bonus for years of work.
        /// </summary>
        /// <param name="date">Date to what calculate years of work.</param>
        /// <returns>Salary only base salary plus bonus for years of work.</returns>
        public double GetBaseSalaryByDate(DateTime date)
        {
            var bonusPercent = yearBonusPercent * GetYearsOfWorkTo(date);
            var bonus = bonusPercent < maxTimeBonus ? bonusPercent : maxTimeBonus;
            return baseSalary + bonus * baseSalary;
        }

        /// <summary>
        ///     Method to get full salary to date.
        /// </summary>
        /// <param name="date">Date until which calculate salary.</param>
        /// <returns>Full salary.</returns>
        public double GetFullSalary(DateTime date)
        {
            return SalaryCalculatorVisitor.GetSalary(this, date);
        }

        /// <summary>
        ///     Returns true if employee has chief.
        /// </summary>
        public bool HasHead()
        {
            return Head != null;
        }

        /// <summary>
        ///     Returns true if transmitted employee is chief of current employee.
        /// </summary>
        /// <param name="employee">Transmitted employee</param>
        /// <returns>If transmitted employee is Boss of this employee</returns>
        public bool HeadIs(BaseEmployee employee)
        {
            if (employee == null)
            {
                throw new NullReferenceException("Employee is not set.");
            }

            return Head == employee;
        }

        /// <summary>
        ///     Removes transmitted employee from chief position for this employee.
        /// </summary>
        /// <param name="employee">Employee to remove from chief position.</param>
        public void RemoveHead()
        {
            Head = null;
        }

        /// <summary>
        ///     Runs logic for transmitted visitor.
        /// </summary>
        internal abstract void Accept(IVisitor visitor);

        /// <summary>
        ///     Adds chief to the employee.
        /// </summary>
        /// <param name="head">Chief for the transmitted employee.</param>
        internal void AddHead(BaseLeadershipPosition head)
        {
            if (head == null)
            {
                throw new NullReferenceException("Chief is not set.");
            }

            if (HasHead())
            {
                throw new ArgumentException("This Employee already has Boss.");
            }

            Head = head;
        }

        /// <summary>
        ///     Returns years of employee's work to transmitted date.
        /// </summary>
        /// <param name="date">Date until which years of work are calculated.</param>
        /// <returns>Years of employee's work until transmitted date.</returns>
        private int GetYearsOfWorkTo(DateTime date)
        {
            var years = Convert.ToInt32((date - startOfWork).TotalDays / ConfigurationData.AVERAGE_DAYS_IN_YEAR);
            years = years > 0 ? years : 0;
            return years = years > 0 ? years : 0;
        }
    }
}