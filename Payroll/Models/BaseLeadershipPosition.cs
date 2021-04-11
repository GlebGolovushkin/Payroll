using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll.Models
{
    /// <summary>
    ///     Class for one person of leadership position.
    /// </summary>
    public abstract class BaseLeadershipPosition : BaseEmployee
    {
        /// <summary>
        ///     All subordinates of the current employee.
        /// </summary>
        private readonly List<BaseEmployee> subordinates;

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        protected BaseLeadershipPosition(DateTime startOfWork, int baseSalary) : base(startOfWork, baseSalary)
        {
            subordinates = new List<BaseEmployee>();
        }

        /// <summary>
        ///     Adds transmitted employees as subordinate to current employee.
        /// </summary>
        /// <param name="employees">Transmitted employee.</param>
        public void AddSubordinates(params BaseEmployee[] employees)
        {
            if (employees == null)
            {
                throw new NullReferenceException("Employee is null.");
            }

            foreach (var employee in employees)
            {
                if (HasSubordinate(employee))
                {
                    throw new ArgumentException("Employee already exist in subordinates.");
                }

                subordinates.Add(employee);
                employee.AddHead(this);
            }
        }

        /// <summary>
        ///     Returns true if transmitted employee is subordinate of this employee.
        /// </summary>
        /// <param name="employee">Transmitted employee.</param>
        /// <returns>Returns true if transmitted employee is subordinate of this employee.</returns>
        public bool HasSubordinate(BaseEmployee employee)
        {
            if (employee == null)
            {
                throw new NullReferenceException("Employee is null.");
            }

            return subordinates.Any(e => e == employee);
        }

        /// <summary>
        ///     Removes transmitted employee from subordinate for this employee.
        /// </summary>
        /// <param name="employee">Employee to remove from subordinates.</param>
        public void RemoveSubordinate(BaseEmployee employee)
        {
            if (employee == null)
            {
                throw new NullReferenceException("Employee is null.");
            }

            if (!HasSubordinate(employee))
            {
                throw new ArgumentException("This employee is not boss of selected one.");
            }

            employee.RemoveHead();
            subordinates.Remove(employee);
        }

        /// <summary>
        ///     Runs logic for transmitted visitor.
        /// </summary>
        internal override void Accept(IVisitor visitor)
        {
            foreach (var subordinate in subordinates)
            {
                subordinate.Accept(visitor);
            }
        }
    }
}