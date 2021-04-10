using System;

namespace Payroll.Models
{
    /// <summary>
    ///     Class that provides logic for one manager
    /// </summary>
    public class Manager : BaseLeadershipPosition
    {
        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public Manager(DateTime startOfWork, int baseSalary) : base(startOfWork, baseSalary)
        { }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        protected override double yearBonusPercent => ConfigurationData.YEAR_MANAGER_BONUS_PERCENT;

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        protected override double maxTimeBonus => ConfigurationData.MAX_MANAGER_BONUS_PERCENT;

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        internal override void Accept(IVisitor visitor)
        {
            visitor.VisitManagerStart(this);
            base.Accept(visitor);
            visitor.VisitManagerEnd(this);
        }
    }
}