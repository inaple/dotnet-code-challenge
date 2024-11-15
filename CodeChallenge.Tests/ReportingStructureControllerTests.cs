using System.Net;
using System.Net.Http;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    /// <summary>
    /// Test cases for ReportingStructureController workflows.  All tests assume the seed data hasn't been changed.
    /// </summary>
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        /// <summary>
        /// Tests getting a ReportingStructure for an employee with at least 1 report.
        /// </summary>
        [TestMethod]
        public void GetReportingStructureById_Returns_Ok_Some_Reports()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";
            var expectedReports = 4;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(expectedFirstName, reportingStructure.Employee.FirstName);
            Assert.AreEqual(expectedLastName, reportingStructure.Employee.LastName);
            Assert.AreEqual(expectedReports, reportingStructure.NumberOfReports);
        }

        /// <summary>
        /// Tests getting a ReportingStructure for an employee with 0 reports.
        /// </summary>
        [TestMethod]
        public void GetReportingStructureById_Returns_Ok_No_Reports()
        {
            // Arrange
            var employeeId = "c0c2293d-16bd-4603-8e08-638a9d18b22c";
            var expectedFirstName = "George";
            var expectedLastName = "Harrison";
            var expectedReports = 0;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(expectedFirstName, reportingStructure.Employee.FirstName);
            Assert.AreEqual(expectedLastName, reportingStructure.Employee.LastName);
            Assert.AreEqual(expectedReports, reportingStructure.NumberOfReports);
        }

        /// <summary>
        /// Tests trying to get a ReportingStructure with a bad input ID
        /// </summary>
        [TestMethod]
        public void GetReportingStructureById_Returns_Not_Found()
        {
            // Test searching for an ID that doesn't exist.
            // Arrange
            var employeeId = "you-will-never-find-this-person";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

            // Test entering an empty string
            // Arrange
            employeeId = "";

            // Execute
            getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
