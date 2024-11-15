using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    /// <summary>
    /// CompensationController specifies the APIs for storing and retrieving Compensation objects for employees
    /// </summary>
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger<CompensationController> _logger;
        private readonly ICompensationService _compensationService;

        /// <summary>
        /// Constructor to initialize debug logger and CompensationService
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="compensationService"></param>
        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        /// <summary>
        /// CreateCompensation stores the input Compensation object to the internal DB
        /// </summary>
        /// <param name="compensation">The employee compensation information</param>
        /// <returns>The newly stored Compensation object or 400 Bad Request if the input is null or an entry already exists for the input employee ID </returns>
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            if (compensation == null)
            {
                _logger.LogDebug($"User-entered Compensation object is null, returning 400 Bad Request");
                return BadRequest("Compensation cannot be null");
            }
                
            _logger.LogDebug($"Received Compensation request for employee with ID '{compensation.Employee.EmployeeId}'");

            // attempt to store the input Compensation object, replacing is not supported for this data type
            Compensation storedCompensation = _compensationService.CreateCompensation(compensation);
            if (storedCompensation == null)
            {
                _logger.LogDebug($"User entered Compensation object with the same Employee ID as an existing entry, returning 400 Bad Request");
                return BadRequest("Compensation already exists for this employee");
            }
            return CreatedAtRoute("getCompensationById", new { id = compensation.Employee.EmployeeId}, compensation);
        }

        /// <summary>
        /// GetCompensationById finds the compensation information for a given employee ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>The employee's compensation information if a matching employee ID is found, NotFound otherwise</returns>
        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            if (id == null)
            {
                _logger.LogDebug($"Input employee ID was null, returning 400 Bad Request");
                return BadRequest("Input employee ID cannot be null");
            }

            _logger.LogDebug($"Received compensation request for {id}");
            Compensation compensation = _compensationService.GetCompensationById(id);
            return compensation == null ? NotFound() : Ok(compensation);
        }
    }
}
