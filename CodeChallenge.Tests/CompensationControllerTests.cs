using System.Net;
using System.Net.Http;
using System.Text;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    /// <summary>
    /// Test cases for CompensationController workflows.  All tests assume the seed data hasn't been changed.
    /// </summary>
    [TestClass]
    public class CompensationControllerTests
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
        /// Tests successfully saving a Compensation object
        /// </summary>
        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            string salary = "12.13";
            string effectiveDate = "1/1/2000";
            var employee = new Employee()
            {
                EmployeeId = employeeId
            };
            var compensation = new Compensation()
            {
                Employee = employee,
                Salary = salary,
                EffectiveDate = effectiveDate
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var returnedCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation.Salary, returnedCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, returnedCompensation.EffectiveDate);
            Assert.AreEqual(compensation.Employee.EmployeeId, returnedCompensation.Employee.EmployeeId);
        }

        /// <summary>
        /// Tests trying to save a Compensation object for an employee ID that already exists in the DB
        /// </summary>
        [TestMethod]
        public void CreateCompensation_Returns_Bad_Request_Already_Exists()
        {
            // Arrange
            string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            string salary = "12.13";
            string effectiveDate = "1/1/2000";
            var employee = new Employee()
            {
                EmployeeId = employeeId
            };
            var compensation = new Compensation()
            {
                Employee = employee,
                Salary = salary,
                EffectiveDate = effectiveDate
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));   // adds employee
            var response = postRequestTask.Result;
            
            postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));   // tries to add the same employee
            response = postRequestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Tests successfully getting a Compensation object
        /// </summary>
        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            string salary = "12.13";
            string effectiveDate = "1/1/2000";
            var employee = new Employee()
            {
                EmployeeId = employeeId
            };
            var compensation = new Compensation()
            {
                Employee = employee,
                Salary = salary,
                EffectiveDate = effectiveDate
            };

            var requestContent = new JsonSerialization().ToJson(compensation);
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var returnedCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation.Salary, returnedCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, returnedCompensation.EffectiveDate);
            Assert.AreEqual(compensation.Employee.EmployeeId, returnedCompensation.Employee.EmployeeId);
        }

        /// <summary>
        /// Tests failing to get a Compensation object due to a non-existent employee ID
        /// </summary>
        [TestMethod]
        public void GetCompensationById_Returns_NotFound()
        {
            // Arrange
            string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            string salary = "12.13";
            string effectiveDate = "1/1/2000";
            var employee = new Employee()
            {
                EmployeeId = employeeId
            };
            var compensation = new Compensation()
            {
                Employee = employee,
                Salary = salary,
                EffectiveDate = effectiveDate
            };
            string badEmployeeId = "you-will-never-find-anyone-with-this-id";

            var requestContent = new JsonSerialization().ToJson(compensation);
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{badEmployeeId}");
            response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
