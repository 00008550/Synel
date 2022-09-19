using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> Create(Employee employee);
        Task<List<Employee>> Getemployees();
        Task<Employee> InsertEmployee(Employee employee);
       
        Task<List<Employee>> FilterAsync(string eSearch);
    }
}
