namespace Payroll.Models
{
    /// <summary>
    ///     Interface for visitors
    /// </summary>
    internal interface IVisitor
    {
        /// <summary>
        ///     Runs logic for employee in Visitor.
        /// </summary>
        void VisitEmployee(Employee employee);

        /// <summary>
        ///     Runs logic for sales in Visitor after all children of current tree node are processed.
        /// </summary>
        void VisitManagerEnd(Manager manager);

        /// <summary>
        ///     Runs logic for manager in Visitor before tree goes deeper.
        /// </summary>
        void VisitManagerStart(Manager manager);

        /// <summary>
        ///     Runs logic for sales in Visitor after all children of current tree node are processed.
        /// </summary>
        void VisitSalesEnd(Sales sales);

        /// <summary>
        ///     Runs logic for sales in Visitor before tree goes deeper.
        /// </summary>
        void VisitSalesStart(Sales sales);
    }
}