using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using X.PagedList;
using DAL.DataFolder;
using DAL.Interfaces;
using BLL.Services;

namespace TestTask.Controllers
{
    public class EmployeesController : Controller
    {
        ImportAndExportServices _importAndExportServices;
        private readonly DataContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;

        public EmployeesController(DataContext context, IEmployeeRepository employeeRepository, IConfiguration configuration, ImportAndExportServices importAndExportServices)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _configuration = configuration;
            _importAndExportServices = importAndExportServices;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.Getemployees();
            var sortedEmployees = employees.OrderBy(x => x.Surname);
              return _context.Employees != null ? 
                          View(sortedEmployees) :
                          Problem("Entity set 'DataContext.Employees'  is null.");
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Payroll_Number == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Payroll_Number,Forename,Surname,Date_of_Birth,Telephone,Mobile,Address,Address_2,Postcode,EMail_Home,Start_Date")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.Create(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Payroll_Number,Forename,Surname,Date_of_Birth,Telephone,Mobile,Address,Address_2,Postcode,EMail_Home,Start_Date")] Employee employee)
        {
            if (id != employee.Payroll_Number)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Payroll_Number))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Payroll_Number == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'DataContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
          return (_context.Employees?.Any(e => e.Payroll_Number == id)).GetValueOrDefault();
        }
        [HttpPost, ActionName("ImportCsv")]
        public async Task<ActionResult> ImportCsv(IFormFile importFile)
        {
            var result = await _importAndExportServices.ImportCsv(importFile);
            if(result == null)
            {
                ModelState.AddModelError("", "Empty file");
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = result.Count + " " + "rows have been affected";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Index(string eSearch)
        {
            ViewData["GetEmployeeDetails"] = eSearch;
            var resutl = await _employeeRepository.FilterAsync(eSearch);
            return View(resutl);
        }
    }
}
