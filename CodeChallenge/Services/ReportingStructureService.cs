using System;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    /// <summary>
    /// ReportingStructureService implements the functionality to create a ReportingStructure object.
    /// </summary>
    public class ReportingStructureService : IReportingStructureService
    {   
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;
        
        /// <summary>
        /// Constructor to initialize private fields
        /// </summary>
        /// <param name="logger">Debug logger</param>
        /// <param name="employeeRepository">EmployeeRepository object to access the employee database</param>
        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// GetReportingStructure builds the ReportingStructure object based on the given employee ID.
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>A ReportingStructure object for the given employee if they're found, null otherwise</returns>
        public ReportingStructure GetReportingStructure(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                _logger.LogDebug($"No ID or empty ID string given");
                return null;
            }

            Employee employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                _logger.LogDebug($"Could not find employee with an ID of {id}");
                return null;
            }

            int reports = CalculateTotalReports(employee);
            return new ReportingStructure { Employee = employee, NumberOfReports = reports };
        }

        /// <summary>
        /// CalculateTotalReports recursively counts all the direct reports for the given employee and returns the count.
        /// </summary>
        /// <param name="employee">The current employee</param>
        /// <returns>The number of direct reports for the given user</returns>
        private int CalculateTotalReports (Employee employee)
        {
            int totalReports = 0;
            if (employee.DirectReports != null)
            {
                foreach (Employee directReport in employee.DirectReports)
                {
                    totalReports++;
                    totalReports += CalculateTotalReports(directReport);
                }
            }
            return totalReports;
        }
    }
}
