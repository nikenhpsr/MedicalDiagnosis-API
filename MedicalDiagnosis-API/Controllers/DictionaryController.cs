using MedicalDiagnosis_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace MedicalDiagnosis_API.Controllers
{
    [ApiController]
    [Route("api/dictionary")]
    public class DictionaryController : ControllerBase
    {
        private readonly MedicalDiagnosisContext _context;
        private readonly List<SpecialityModel> _specialties; // Assume this is populated elsewhere
        private readonly List<Icd10RecordModel> _icdRecords; // Assume this is populated elsewhere

        public DictionaryController()
        {
            // Mock data initialization for demonstration purposes
            _specialties = new List<SpecialityModel>
            {
                new SpecialityModel { Id = Guid.NewGuid(), Name = "Cardiology" },
                new SpecialityModel { Id = Guid.NewGuid(), Name = "Neurology" }
                // Add more specialties as needed
            };

            _icdRecords = new List<Icd10RecordModel>
            {
                new Icd10RecordModel { Id = Guid.NewGuid(), Code = "I10", Name = "Hypertension" },
                new Icd10RecordModel { Id = Guid.NewGuid(), Code = "E11", Name = "Type 2 diabetes mellitus" }
                // Add more ICD-10 records as needed
            };
        }

        [HttpGet("speciality")]
        public IActionResult GetSpecialties([FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var filteredSpecialties = _specialties
                .Where(s => string.IsNullOrWhiteSpace(name) || s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return Ok(new
            {
                specialties = filteredSpecialties,
                pagination = new { size, count = filteredSpecialties.Count, current = page }
            });
        }

        [HttpGet("icd10")]
        public IActionResult SearchIcd10([FromQuery] string request, [FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var filteredRecords = _icdRecords
                .Where(r => string.IsNullOrWhiteSpace(request) || r.Name.Contains(request, StringComparison.OrdinalIgnoreCase) || r.Code.Contains(request))
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return Ok(new
            {
                records = filteredRecords,
                pagination = new { size, count = filteredRecords.Count, current = page }
            });
        }

        [HttpGet("icd10/roots")]
        public IActionResult GetIcd10Roots()
        {
            // Assuming root ICD-10 elements are those with a code ending in "0"
            var rootIcdRecords = _icdRecords
                .Where(r => r.Code.EndsWith("0"))
                .ToList();

            return Ok(rootIcdRecords);
        }
    }
}
