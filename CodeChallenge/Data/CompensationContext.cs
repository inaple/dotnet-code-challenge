using Microsoft.EntityFrameworkCore;
using CodeChallenge.Models;

namespace CodeChallenge.Data
{
    /// <summary>
    /// CompensationContext provides the persistence layer access for storing and retrieving Compensation objects
    /// </summary>
    public class CompensationContext : DbContext
    {
        /// <summary>
        /// Default constructor to initialize the DbContext
        /// </summary>
        /// <param name="options"></param>
        public CompensationContext(DbContextOptions<CompensationContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// Contains all the stored Compensation objects
        /// </summary>
        public DbSet<Compensation> Compensations 
        { get; set; }
    }
}
