using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    /// <summary>
    /// ReportingStructureController specifies the APIs for getting ReportingStructure objects for employees
    /// </summary>
    [ApiController]
    [Route("api/reportingstructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        /// <summary>
        /// Constructor to initialize the logger and the reporting structure service
        /// </summary>
        /// <param name="logger">Debug logger</param>
        /// <param name="reportingStructureService">Service containing the functionality required for reporting structures</param>
        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        /// <summary>
        /// GetReportingStructureById creates and returns the ReportingStructure for a given employee.
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>The ReportingStructure object for the given employee, NotFound message if the employee ID could not be found</returns>
        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            _logger.LogDebug($"Received reporting structure GET request for {id}");

            ReportingStructure rs = _reportingStructureService.GetReportingStructure(id);
            return rs != null ? Ok(rs) : NotFound();
        }
    }
}
