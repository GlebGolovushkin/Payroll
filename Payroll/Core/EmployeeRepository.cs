using System;
using System.Collections.Generic;
using System.Linq;
using Payroll.Models;
using Payroll.Models.Visitor;

namespace Payroll.Core
{
    /// <summary>
    ///     Class for employee.
    /// </summary>
    public class EmployeeRepository
    {
        /// <summary>
        ///     Ctor.
        /// </summary>
        public EmployeeRepository()
        {
            Employees = new List<BaseEmployee>();
        }

        /// <summary>
        ///     All employees contained in repository.
        /// </summary>
        public List<BaseEmployee> Employees { get; }

        /// <summary>
        ///     Adds transmitted employees to repository.
        /// </summary>
        /// <param name="employees">Employee to add in repository.</param>
        public void AddEmployees(params BaseEmployee[] employees)
        {
            foreach (var employee in employees)
            {
                if (ContainsEmployee(employee))
                {
                    throw new NullReferenceException("Employee already exists in company.");
                }

                Employees.Add(employee);
            }
        }

        /// <summary>
        ///     Method that returns true if repository contains transmitted employee.
        /// </summary>
        /// <param name="employee">Employee to test if he exist in current repository.</param>
        /// <returns>True if transmitted employee exists in current repository.</returns>
        public bool ContainsEmployee(BaseEmployee employee)
        {
            return Employees.Any(e => e == employee);
        }

        /// <summary>
        ///     Method to get sum of salaries in company by the transmitted date.
        /// </summary>
        /// <param name="date">Date until which to calculate the salary.</param>
        /// <returns>Sum of salaries of all employees in repository.</returns>
        public double GetSumOfEmployesSalariesForDate(DateTime date)
        {
            return CompanySalaryCalculatorVisitor.GetSumOfEmployesSalaries(this, date);
        }

        /// <summary>
        ///     Method to remove transmitted employee from repository.
        /// </summary>
        /// <param name="employeeToRemove">Employee to remove from repository.</param>
        public void RemoveEmployee(BaseEmployee employeeToRemove)
        {
            if (!ContainsEmployee(employeeToRemove))
            {
                throw new NullReferenceException("Employee does not exist in company.");
            }

            Employees.Remove(employeeToRemove);
            var employeeHead = employeeToRemove.Head;
            foreach (var employee in Employees)
            {
                if (employeeToRemove is BaseLeadershipPosition employeeToRemoveAsHead &&
                    employeeToRemoveAsHead.HasSubordinate(employee))
                {
                    employeeToRemoveAsHead.RemoveSubordinate(employee);
                }

                if (employeeToRemove.HasHead())
                {
                    employeeHead.AddSubordinates(employee);
                }
            }

            if (employeeToRemove.HasHead())
            {
                employeeHead.RemoveSubordinate(employeeToRemove);
            }
        }
    }
}