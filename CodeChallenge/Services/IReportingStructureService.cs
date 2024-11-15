using CodeChallenge.Models;
using System;

namespace CodeChallenge.Services
{
    /// <summary>
    /// Interface for the ReportingStructureService class.
    /// </summary>
    public interface IReportingStructureService
    {
        /// <summary>
        /// Gets the ReportingStructure object for the given employee ID.
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>The ReportingStructure for the given employee</returns>
        ReportingStructure GetReportingStructure(String id);
    }
}
