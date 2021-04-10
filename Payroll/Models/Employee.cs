using System;

namespace Payroll.Models
{
    /// <summary>
    ///     Class that provides logic for one employee
    /// </summary>
    public class Employee : BaseEmployee
    {
        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public Employee(DateTime startOfWork, int baseSalary) : base(startOfWork, baseSalary)
        {
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        protected override double yearBonusPercent => ConfigurationData.YEAR_EMPLOYEE_BONUS_PERCENT;

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        protected override double maxTimeBonus => ConfigurationData.MAX_EMPLOYEE_BONUS_PERCENT;

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        internal override void Accept(IVisitor visitor)
        {
            visitor.VisitEmployee(this);
        }
    }
}