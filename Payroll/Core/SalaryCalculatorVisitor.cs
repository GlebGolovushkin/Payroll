using System;
using System.Collections.Generic;

namespace Payroll.Models.Visitor
{
    /// <summary>
    ///     Class for calculator, that calculates salary of one employee.
    /// </summary>
    internal class SalaryCalculatorVisitor : IVisitor
    {
        // Stack of salaries sum by every tree level.
        private readonly Stack<double> sumOfSalariesByLevel = new Stack<double>();
        // Stack of salaries sum by every sales tree node.
        private readonly Stack<double> sumOfSalesSubordinatesSalariesByNode = new Stack<double>(); //TODO ctor
        protected DateTime dateToCalculate;
        protected double salary;

        /// <summary>
        ///     Method to get employees salary and update info with sum of sales.
        /// </summary>
        public virtual void VisitEmployee(Employee employee)
        {
            salary = employee.GetBaseSalaryByDate(dateToCalculate);
            // Add salary to all salaries of current node.
            UpdateSalesSubordinatesSalariesByNodeStack(salary);
            // Add salary to all salaries of current tree level.
            UpdateSumOfSalariesByLevelStack(salary);
        }

        /// <summary>
        ///     Method to start collecting data for manager.
        /// </summary>
        public virtual void VisitManagerStart(Manager manager)
        {
            // Pushing new value to sum of salaries stack because of new level of tree is creating.
            sumOfSalariesByLevel.Push(0);
        }

        /// <summary>
        ///     Method to get managers salary and update info with sum of sales.
        /// </summary>
        public virtual void VisitManagerEnd(Manager manager)
        {
            salary = manager.GetBaseSalaryByDate(dateToCalculate);
            // Sum of manager's subordinates salaries.
            salary += sumOfSalariesByLevel.Pop() * ConfigurationData.MANGER_BONUS_COEFF;

            if (sumOfSalariesByLevel.Count > 0)
            {
                // Add salary to its head if manager has some manager upper
                var headSalaries = sumOfSalariesByLevel.Pop();
                sumOfSalariesByLevel.Push(headSalaries + salary);
            }

            // Add salary to all salaries of current node.
            UpdateSalesSubordinatesSalariesByNodeStack(salary);
        }

        /// <summary>
        ///     Method to init collecting data for sales.
        /// </summary>
        public virtual void VisitSalesStart(Sales sales)
        {
            // Adding new sales salaries node.
            sumOfSalesSubordinatesSalariesByNode.Push(0);
            // Adding new tree level.
            sumOfSalariesByLevel.Push(0);
        }

        /// <summary>
        ///     Method to get sales salary and update info for head sales and  head managers.
        /// </summary>
        public virtual void VisitSalesEnd(Sales sales)
        {
            // Base salary.
            salary = sales.GetBaseSalaryByDate(dateToCalculate);
            var subordinatesSalaries = sumOfSalesSubordinatesSalariesByNode.Pop();
            // Sum of sales' subordinates salaries.
            salary += subordinatesSalaries * ConfigurationData.SALES_BONUS_COEFF;
            if (sumOfSalesSubordinatesSalariesByNode.Count > 0)
            {
                // Push sum of salaries upper if any of sales exist upper in tree.
                var headSalaries = sumOfSalesSubordinatesSalariesByNode.Pop();
                sumOfSalesSubordinatesSalariesByNode.Push(subordinatesSalaries + salary + headSalaries);
            }

            // Deleting one node in sum of salaries by tree level, because level is ended.
            sumOfSalariesByLevel.Pop();
            if (sumOfSalariesByLevel.Count > 0)
            {
                // Push sum upper if any of managers exist upper.
                var headSalaries = sumOfSalariesByLevel.Pop();
                sumOfSalariesByLevel.Push(headSalaries + salary);
            }
        }

        /// <summary>
        ///     Method to get salary for one employee by transmitted date.
        /// </summary>
        /// <param name="employee">Employee to calculate salary.</param>
        /// <param name="date">Date until which to calculate the salary.</param>
        /// <returns>Employee's salary.</returns>
        internal double GetSalary(BaseEmployee employee, DateTime date)
        {
            dateToCalculate = date;
            employee.Accept(this);
            return salary;
        }

        /// <summary>
        ///     Method to update any salary stack.
        /// </summary>
        private void UpdateSalaryStack(Stack<double> stack, double currentSalary)
        {
            if (stack.Count > 0)
            {
                var allSalary = stack.Pop();
                stack.Push(currentSalary + allSalary);
            }
        }

        /// <summary>
        ///     Method to update sales subordinates salaries by node stack.
        /// </summary>
        private void UpdateSalesSubordinatesSalariesByNodeStack(double currentSalary)
        {
            UpdateSalaryStack(sumOfSalesSubordinatesSalariesByNode, currentSalary);
        }

        /// <summary>
        ///     Method to update sum of salaries by level stack.
        /// </summary>
        private void UpdateSumOfSalariesByLevelStack(double currentSalary)
        {
            UpdateSalaryStack(sumOfSalariesByLevel, currentSalary);
        }
    }
}