namespace CodeChallenge.Models
{
    /// <summary>
    /// ReportingStructure models the information about an employee and their total number of subordinates
    /// </summary>
    public class ReportingStructure
    {
        /// <summary>
        /// The Employee object containing all information about the employee
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// The total number of reports for the employee
        /// </summary>
        public int NumberOfReports { get; set; }
    }
}