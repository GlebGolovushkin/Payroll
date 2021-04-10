namespace Payroll
{
    /// <summary>
    ///     Class with configuration data.
    /// </summary>
    public static class ConfigurationData
    {
        // Bonus coefficient for manger's salary based on every salary of his first level subordinate.
        public const double MANGER_BONUS_COEFF = 0.005;

        // Bonus coefficient for sales' salary based on every salary of his subordinate.
        public const double SALES_BONUS_COEFF = 0.003;

        // Bonus coefficient for employee's salary based on every year of his work.
        public const double YEAR_EMPLOYEE_BONUS_PERCENT = 0.03;

        // Bonus coefficient for manager's salary based on every year of his work.
        public const double YEAR_MANAGER_BONUS_PERCENT = 0.05;

        // Bonus coefficient for sales' salary based on every year of his work.
        public const double YEAR_SALES_BONUS_PERCENT = 0.01;

        // Max bonus coefficient for employee's salary based on every year of his work.
        public const double MAX_EMPLOYEE_BONUS_PERCENT = 0.3;

        // Max bonus coefficient for manager's salary based on every year of his work.
        public const double MAX_MANAGER_BONUS_PERCENT = 0.4;

        // Max bonus coefficient for sales' salary based on every year of his work.
        public const double MAX_SALES_BONUS_PERCENT = 0.35;

        // Average days in year.
        public const double AVERAGE_DAYS_IN_YEAR = 365.2425;
    }
}