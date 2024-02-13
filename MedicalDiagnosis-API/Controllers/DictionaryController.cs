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
    }
}
