using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        /// <summary>
        /// Create stores the given Compensation object in the persistence layer
        /// </summary>
        /// <param name="compensation">The Compensation object to store</param>
        /// <returns>The stored Compensation object</returns>
        public Compensation Add(Compensation compensation);

        /// <summary>
        /// GetById finds the Compensation object with the matching employee ID
        /// </summary>
        /// <param name="id">The employee ID</param>
        /// <returns>The Compensation object for the given employee if found, null if there was no ID match</returns>
        public Compensation GetById(string id);

        /// <summary>
        /// SaveAsync saves the internal DbSet of Compensation objects
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
