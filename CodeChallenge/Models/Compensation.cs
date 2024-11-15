using System;

namespace CodeChallenge.Models
{
    /// <summary>
    /// Compensation models the employee's compensation information
    /// </summary>
    public class Compensation
    {
        /// <summary>
        /// The employee information
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// The employee's salary
        /// </summary>
        public String Salary { get; set; }

        /// <summary>
        /// The employee's start date
        /// </summary>
        public String EffectiveDate { get; set; }

        /// <summary>
        /// Unique Id generated for this Compensation object
        /// </summary>
        public string CompensationId { get; set; }
    }
}
