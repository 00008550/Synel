using DAL.DataFolder;
using DAL.Entities;
using DAL.Implementation;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TestTask.UnitTests
{
    public class UnitTest1
    {

        [Fact]
        public async void InsertEmployee()
        {
            
           
            DbContextOptionsBuilder<DataContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);
            DataContext ctx = new(optionsBuilder.Options);
            IEmployeeRepository _employeeRepository = new EmployeeRepository(ctx);
            Employee employee = new Employee { Payroll_Number = Guid.NewGuid().ToString(),
            Forename = "Test",
            Surname = "test",
            Date_of_Birth = DateTime.Now.ToString(),
            Telephone =23232323,
            Mobile = 213213123,
            Address = "sadwad",
            Address_2 = "awdwadadw",
            Postcode = "SAwaRAW",
            EMail_Home ="w1232132",
            Start_Date = DateTime.Now.ToString()};

            await _employeeRepository.InsertEmployee(employee);

            var result = await ctx.Employees.FindAsync(employee.Payroll_Number);

            ctx.Remove(employee);

            Assert.NotNull(result);
            Assert.Equal(employee,result);


        }
        [Fact]
        public async void FilterAsync()
        {
            DbContextOptionsBuilder<DataContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);
            DataContext ctx = new(optionsBuilder.Options);
            IEmployeeRepository _employeeRepository = new EmployeeRepository(ctx);
            Employee employee = new Employee
            {
                Payroll_Number = Guid.NewGuid().ToString(),
                Forename = "Test",
                Surname = "test",
                Date_of_Birth = DateTime.Now.ToString(),
                Telephone = 23232323,
                Mobile = 213213123,
                Address = "sadwad",
                Address_2 = "awdwadadw",
                Postcode = "SAwaRAW",
                EMail_Home = "w1232132",
                Start_Date = DateTime.Now.ToString()
            };
            List<Employee> employees = new List<Employee>();
            string search = "test";
            await _employeeRepository.InsertEmployee(employee);
            employees.Add(employee);

            var result = await _employeeRepository.FilterAsync(search);
            ctx.Remove(employee);
            Assert.Equal(employees.ToString(),result.ToString());
            

        }
    }
}