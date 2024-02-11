using Microsoft.AspNetCore.Mvc;

namespace MedicalDiagnosis_API.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : ControllerBase
    {
        // GET: api/report/icdrootsreport
        [HttpGet("icdrootsreport")]
        public IActionResult GetIcdRootsReport([FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] List<string> icdRoots = null)
        {
            // Validate the input parameters
            if (end < start)
            {
                return BadRequest("End date must be after start date.");
            }

            // Simulate fetching data based on the provided parameters
            // In a real application, this would involve querying your database or data source to retrieve relevant records
            var reportData = GenerateReportData(start, end, icdRoots);

            return Ok(reportData);
        }

        private object GenerateReportData(DateTime start, DateTime end, List<string> icdRoots)
        {
            // Simulate generating report data. This should be replaced with actual logic to fetch and aggregate data.
            var records = new List<object>();
            var summaryByRoot = new Dictionary<string, int>();

            // Example data generation logic
            if (icdRoots == null || !icdRoots.Any())
            {
                icdRoots = new List<string> { "Root1", "Root2", "Root3" }; // Assume these are the ICD-10 roots if none are specified
            }

            foreach (var root in icdRoots)
            {
                var visitsByRoot = new Random().Next(1, 100); // Simulate the number of visits for this root
                summaryByRoot.Add(root, visitsByRoot);

                // Simulate patient visit records
                records.Add(new
                {
                    PatientName = "John Doe",
                    PatientBirthdate = DateTime.UtcNow.AddYears(-30),
                    Gender = "Male",
                    VisitsByRoot = new Dictionary<string, int> { { root, visitsByRoot } }
                });
            }

            var report = new
            {
                Filters = new { Start = start, End = end, IcdRoots = icdRoots },
                Records = records,
                SummaryByRoot = summaryByRoot
            };

            return report;
        }
    }
}

