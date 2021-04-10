using System;

namespace DemoApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Short demonstration.
            var demo = new Demo();
            var employeeRepository = demo.InitEmployeeRepository();
            Console.WriteLine(employeeRepository.Employees[0].GetFullSalary(DateTime.Now.AddYears(2)));
            Console.WriteLine(employeeRepository.GetSumOfEmployesSalariesForDate(DateTime.Now.AddYears(5)));
            Console.ReadKey();
        }
    }
}