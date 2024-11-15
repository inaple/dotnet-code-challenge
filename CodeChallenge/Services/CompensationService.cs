using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    /// <summary>
    /// CompensationService implements the functionality to read and write Compensation objects
    /// </summary>
    public class CompensationService : ICompensationService
    {
        private readonly ILogger<CompensationService> _logger;
        private readonly ICompensationRepository _compensationRepository;

        /// <summary>
        /// This constructor initializes the debug logger and the CompensationRepository
        /// </summary>
        /// <param name="logger">The debug logger</param>
        /// <param name="compensationRepository">The CompensationRepository</param>
        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
        }

        /// <summary>
        /// CreateCompensation stores the input Compensation object to the DB
        /// </summary>
        /// <param name="compensation">The input Compensation object</param>
        /// <returns>null if there was an existing entry for the input employee, otherwise the input Compensation object</returns>
        public Compensation CreateCompensation(Compensation compensation)
        {
            Compensation addedCompensation = _compensationRepository.Add(compensation);
            if (addedCompensation != null)
            {
                _compensationRepository.SaveAsync().Wait();
            }
            return addedCompensation;
        }

        /// <summary>
        /// GetCompensationById returns the Compensation object for a given employee ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>The matching Compensation object for the given employee ID, null if a matching ID was not found</returns>
        public Compensation GetCompensationById(string id)
        {
            Compensation compensation = null;
            if (!String.IsNullOrEmpty(id))
            {
                compensation = _compensationRepository.GetById(id);
            }
            return compensation;
        }
    }
}
