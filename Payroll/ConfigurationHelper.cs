namespace Payroll
{
    /// <summary>
    ///     Class with configuration data.
    /// </summary>
    public static class ConfigurationData
    {
        /// <summary>
        ///     Bonus coefficient for manger's salary based on every salary of his first level subordinate.
        /// </summary>
        public const double MANGER_BONUS_COEFF = 0.005;

        /// <summary>
        ///     Bonus coefficient for sales' salary based on every salary of his subordinate.
        /// </summary>
        public const double SALES_BONUS_COEFF = 0.003;

        /// <summary>
        ///     Bonus coefficient for employee's salary based on every year of his work.
        /// </summary>
        public const double YEAR_EMPLOYEE_BONUS_PERCENT = 0.03;

        /// <summary>
        ///     Bonus coefficient for manager's salary based on every year of his work.
        /// </summary>
        public const double YEAR_MANAGER_BONUS_PERCENT = 0.05;

        /// <summary>
        ///     Bonus coefficient for sales' salary based on every year of his work.
        /// </summary>
        public const double YEAR_SALES_BONUS_PERCENT = 0.01;

        /// <summary>
        ///     Max bonus coefficient for employee's salary based on every year of his work.
        /// </summary>
        public const double MAX_EMPLOYEE_BONUS_PERCENT = 0.3;

        /// <summary>
        ///     Max bonus coefficient for manager's salary based on every year of his work.
        /// </summary>
        public const double MAX_MANAGER_BONUS_PERCENT = 0.4;

        /// <summary>
        ///     Max bonus coefficient for sales' salary based on every year of his work.
        /// </summary>
        public const double MAX_SALES_BONUS_PERCENT = 0.35;

        /// <summary>
        ///     Average days in year.
        /// </summary>
        public const double AVERAGE_DAYS_IN_YEAR = 365.2425;
    }
}