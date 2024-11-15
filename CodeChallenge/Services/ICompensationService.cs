using CodeChallenge.Models;
using System;
namespace CodeChallenge.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICompensationService
    {
        /// <summary>
        /// Create stores a Compensation object in the persistence layer
        /// </summary>
        /// <param name="compensation">The Compensation information to store</param>
        /// <returns>The newly stored Compensation information</returns>
        Compensation CreateCompensation(Compensation compensation);

        /// <summary>
        /// GetCompensationById returns the Compensation information for the given employee ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>The Compensation information for the given employee if found, null otherwise</returns>
        Compensation GetCompensationById(String id);
    }
}
