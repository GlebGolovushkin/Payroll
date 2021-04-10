using System;

namespace Payroll.Models
{
    /// <summary>
    ///     Class that provides logic for one sale
    /// </summary>
    public class Sales : BaseLeadershipPosition
    {
        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public Sales(DateTime startOfWork, int baseSalary) : base(startOfWork, baseSalary)
        { }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        protected override double yearBonusPercent => ConfigurationData.YEAR_SALES_BONUS_PERCENT;

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        protected override double maxTimeBonus => ConfigurationData.MAX_SALES_BONUS_PERCENT;

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        internal override void Accept(IVisitor visitor)
        {
            visitor.VisitSalesStart(this);
            base.Accept(visitor);
            visitor.VisitSalesEnd(this);
        }
    }
}