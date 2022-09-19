using CsvHelper;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BLL.Services
{
    public class ImportAndExportServices
    {
        IEmployeeRepository _employeeRepository;

        public ImportAndExportServices(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> ImportCsv(IFormFile importFile)
        {
            var employees = new List<Employee>();
            if (importFile != null)
            {
                using (var stream = importFile.OpenReadStream())
                using (var reader = new StreamReader(stream))
                {
                    var serializer = new CsvReader(reader, CultureInfo.InvariantCulture);
                    employees = serializer.GetRecords<Employee>().ToList<Employee>();
                }

                foreach (var employee in employees)
                    await _employeeRepository.InsertEmployee(employee);
                return employees;
            }
            else
            {
                 return employees;
            }
        }
    }
}

