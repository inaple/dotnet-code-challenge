using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;

            // I encountered a bug where the DirectReports would always be null when getting an employee's info by their ID.  I traced the data seeding
            // and saw that the DirectReports were being stored correctly initially, but trying to retrieve them later caused them to become null.
            // While debugging, I discovered triggering IEnumerable by hovering over the 'Results View' of Employees would fix the DirectReports.
            // Calling ToList triggers IEnumerable which fixes the DirectReports within the Employees DbSet.  I tried to find a good explanation
            // as to why this was happening or how to fix it but I couldn't find anything better than this.
            // This bug was happening without any changes to the code I was provided so I'm not sure if it's an issue with my local environment or the code itself.
            _ = _employeeContext.Employees.ToList<Employee>();
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
