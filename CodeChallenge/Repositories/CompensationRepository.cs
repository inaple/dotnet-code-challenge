using System;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Repositories
{
    /// <summary>
    /// CompensationRepository implements the functionality for reading and writing Compensation objects to the internal DB
    /// </summary>
    public class CompensationRepository : ICompensationRepository
    {
        private readonly ILogger<CompensationRepository> _logger;
        private CompensationContext _compensationContext;

        /// <summary>
        /// This constructor initializes the logger and CompensationContext
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="compensationContext"></param>
        public CompensationRepository(ILogger<CompensationRepository> logger, CompensationContext compensationContext)
        {
            _logger = logger;
            _compensationContext = compensationContext;
        }

        /// <summary>
        /// Add stores a Compensation object to the Compensations DbSet
        /// </summary>
        /// <param name="compensation">The Compensation information</param>
        /// <returns>The stored Compensation object, or null if there is an existing entry for the given employee</returns>
        public Compensation Add(Compensation compensation)
        {
            // Check if the compensation already exists for this employee as Compensations cannot be updated
            if (_compensationContext.Compensations.Any(c => c.Employee.EmployeeId == compensation.Employee.EmployeeId))
            {
                _logger.LogDebug($"Employee compensation info already exists in the DB");
                return null;
            }
            compensation.CompensationId = Guid.NewGuid().ToString();    // creates a unique Key for the Compensation object
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        /// <summary>
        /// GetById searches for a Compensation object based on an employee ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>The Compensation object with the matching employee ID</returns>
        public Compensation GetById(string id)
        {
            return _compensationContext.Compensations.Include(x => x.Employee).SingleOrDefault(c => c.Employee.EmployeeId == id);
        }

        /// <summary>
        /// SaveAsync saves the changes to the Compensations DbSet to the DB
        /// </summary>
        /// <returns>A Task object containing the number of entries saved</returns>
        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
