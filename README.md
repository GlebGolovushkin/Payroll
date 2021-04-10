# Payroll

## Description
This application is designed to work with company employees. In it we have an employee tree (employee trees).
Which bypasses the algorithm and performs the task you need. For example, getting a salary or getting all the salaries of the employee tree. The problem was solved using the visitor pattern. This pattern was used because -  for each new task in the future, in which a tree traversal will be required, you will not have to change and rebuild the architecture, but only add a new visitor inherited from the IVisitor interface.

## Pluses
- Flexible architecture for feature tasks in tree.
- Flexible architecture for feature new employees and types of enployees in tree.
- The employee tree is bypassed once.
- No employee is billed twice or more.
- Used standart pattern - so another programmers will understand the logic easily.

## Minuses
- Too big architecture. Did more than was asked. If the aim was to resolve as fast as it is possible - so should be used just recursive method.
- Too difficult architecture. If developer doesn't know visitor pattern - I think he will lose much time to understand how it works.
- Else is said in next paragraph.

## What can be improved or changed
- First of all to work with this application is difficult. To add employee one by one with setting up date of work start and salary. Than add linking between them is not too frindly. As for me - it is better to develope file parsers (xml/xls). I think in this case it will be easier to use it in different companies.
- It is needed to create tests for visitors using mocks.
- Adding much more exceptions and tests. (checking for null).
- Creating helper for tests to init repos and employees.
- Relooking threw access modifires (I am shure it should be in another variant).

## Demo
If you want to create company with 14 employees like on image you should run following code:
```
            // Creating employees with date of employment and base salary without any bonuses.
            Sales s = new Sales(DateTime.Now.AddYears(- 11), 10000);
            Manager m1 = new Manager(DateTime.Now.AddYears(- 10), 8500);
            Employee e1 = new Employee(DateTime.Now.AddYears(- 8), 7300);
            Sales s1 = new Sales(DateTime.Now.AddYears(-5), 6000);
            Employee e2 = new Employee(DateTime.Now.AddYears(-2), 4000);
            Manager m2 = new Manager(DateTime.Now.AddYears(- 2), 4000);
            Manager m3 = new Manager(DateTime.Now.AddYears(- 1), 3700);
            Employee e3 = new Employee(DateTime.Now.AddYears(- 2), 4000);
            Employee e4 = new Employee(DateTime.Now.AddYears(- 3), 4500);
            Manager m4 = new Manager(DateTime.Now.AddYears(- 13), 9500);
            Manager m5 = new Manager(DateTime.Now.AddYears(- 7), 7300);
            Employee e5 = new Employee(DateTime.Now.AddYears(- 2), 3000);
            Employee e6 = new Employee(DateTime.Now.AddYears(- 1), 2000);
            Manager m6 = new Manager(DateTime.Now, 1800);

            // Linking all employees.
            s.AddSubordinates(m1, e1, s1);
            m1.AddSubordinates(e2, m2);
            s1.AddSubordinates(m3);
            m3.AddSubordinates(e3);
            m3.AddSubordinates(e4);
            m4.AddSubordinates(m5, e5);
            m5.AddSubordinates(e6, m6);

            // Creating employee repository.
            EmployeeRepository repository = new EmployeeRepository();

            // Linking employee repository.
            repository.AddEmployees(s, s1, m1, m2, m3, m4, m5, m6, e1, e2, e3, e4, e5, e6);

            // Getting salary with all bonuses for "s" employee.
            s.GetFullSalary(DateTime.Now.AddYears(2));

            // Getting sum of all salaries in company.
            repository.GetSumOfEmployesSalariesForDate(DateTime.Now.AddYears(5));
```
## Image of the following company structure
![N|Solid](https://raw.githubusercontent.com/GlebGolovushkin/Payroll/main/Resources/Images/CompanyEmployeesTree.png?token=AFU7SA34G7ESH6YO2H7F2FLAOHGX6)