using System;
using Payroll.Core;

namespace Payroll.Models.Visitor
{
    /// <summary>
    ///     Class that calculates employeeRepository's salary.
    /// </summary>
    internal class CompanySalaryCalculatorVisitor : SalaryCalculatorVisitor
    {
        // Stores salary of all processed employees.
        private double resultSalary { get; set; }

        /// <summary>
        ///     <inheritdoc />
        ///     And then copies gotten salary to result salary.
        /// </summary>
        public override void VisitEmployee(Employee employee)
        {
            base.VisitEmployee(employee);
            resultSalary += salary;
        }

        /// <summary>
        ///     <inheritdoc />
        ///     And then copies gotten salary to result salary.
        /// </summary>
        public override void VisitManagerEnd(Manager manager)
        {
            base.VisitManagerEnd(manager);
            resultSalary += salary;
        }

        /// <summary>
        ///     <inheritdoc />
        ///     And then copies gotten salary to result salary.
        /// </summary>
        public override void VisitSalesEnd(Sales sales)
        {
            base.VisitSalesEnd(sales);
            resultSalary += salary;
        }

        /// <summary>
        ///     Method to get all employees salary in one employeeRepository by transmitted date.
        /// </summary>
        /// <param name="employeeRepository">Repository with employees.</param>
        /// <param name="date">Date until which to calculate the salary.</param>
        /// <returns>Sum of Employees salary in selected employeeRepository.</returns>
        internal double GetSumOfEmployesSalaries(EmployeeRepository employeeRepository, DateTime date)
        {
            dateToCalculate = date;
            foreach (var employee in employeeRepository.Employees)
                //Working only with the tops of trees so that there is no duplication in salary calculation.
                if (!employee.HasHead())
                    employee.Accept(this);

            return resultSalary;
        }
    }
}