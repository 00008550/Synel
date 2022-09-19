using DAL.DataFolder;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Employee>> Getemployees()
        {
             return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> InsertEmployee(Employee employee)
        {
            if(_context.Employees.SingleOrDefault(x=>x.Payroll_Number==employee.Payroll_Number) == null )
            {
                await _context.AddAsync(employee);
                await _context.SaveChangesAsync();
                
            }
            return employee;
            
        }
        public async Task<List<Employee>> FilterAsync(string eSearch)
        {
            var empquery = from x in _context.Employees select x;
            if (!string.IsNullOrEmpty(eSearch))
            {
                empquery = empquery.Where(x => x.Payroll_Number.Contains(eSearch) || x.Surname.Contains(eSearch));
            }
            var result = await empquery.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Employee> Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}
