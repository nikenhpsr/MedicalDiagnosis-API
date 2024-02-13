using MedicalDiagnosis_API.Data;
using Microsoft.AspNetCore.Mvc;

namespace MedicalDiagnosis_API.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController : ControllerBase
    {
        private readonly MedicalDiagnosisContext _context;

        public PatientController(MedicalDiagnosisContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateNewPatient([FromBody] PatientCreateModel patientModel)
        {
            var newPatient = new PatientModel
            {
                Name = patientModel.Name,
                Birthday = patientModel.Birthday,
                Gender = patientModel.Gender
            };

            _context.Patients.Add(newPatient);
            _context.SaveChanges();

            return Ok(newPatient.Id);
        }

        [HttpGet]
        public IActionResult GetPatientsList([FromQuery] string name, [FromQuery] List<string> conclusions, [FromQuery] string sorting, [FromQuery] bool scheduledVisits = false, [FromQuery] bool onlyMine = false, [FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            // Add more filters and sorting as needed...

            var totalPatients = query.Count();
            var patients = query.Skip((page - 1) * size).Take(size).ToList();

            return Ok(new
            {
                patients,
                pagination = new { size, count = totalPatients, current = page }
            });
        }

        [HttpPost("{id}/inspections")]
        public IActionResult CreateInspectionForPatient([FromRoute] Guid id, [FromBody] InspectionModel inspectionModel)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            // Assuming InspectionModel is mapped correctly to your Inspection entity
            var newInspection = new InspectionModel
            {
                Id = Guid.NewGuid(), // Generate a new ID for the inspection
                CreateTime = DateTime.UtcNow, // Set the creation time to the current time
                Date = inspectionModel.Date,
                Anamnesis = inspectionModel.Anamnesis,
                Complaints = inspectionModel.Complaints,
                Treatment = inspectionModel.Treatment,
                Conclusion = inspectionModel.Conclusion,
                NextVisitDate = inspectionModel.NextVisitDate,
                PreviousInspectionId = inspectionModel.PreviousInspectionId
            };

            // Add the new inspection to the patient
            patient.Inspections.Add(newInspection);
            _context.SaveChanges();

            return Ok(newInspection.Id);
        }

        [HttpGet("{id}/inspections")]
        public IActionResult GetPatientMedicalInspections([FromRoute] Guid id, [FromQuery] List<string> icdRoots = null, [FromQuery] int page = 1, [FromQuery] int size = 5, [FromQuery] bool grouped = false)
        {
            var patient = _context.Patients.Include(p => p.Inspections).FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            var inspections = patient.Inspections.AsQueryable();

            // Add filters and grouping logic as needed...

            var totalInspections = inspections.Count();
            var paginatedInspections = inspections.Skip((page - 1) * size).Take(size).ToList();

            return Ok(new
            {
                inspections = paginatedInspections,
                pagination = new { size, count = totalInspections, current = page }
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetPatientCard([FromRoute] Guid id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpGet("{id}/inspections/search")]
        public IActionResult SearchPatientMedicalInspections([FromRoute] Guid id, [FromQuery] string request)
        {
            var patient = _context.Patients.Include(p => p.Inspections).FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            var matchingInspections = patient.Inspections
                .Where(i => i.Anamnesis.Contains(request) || i.Treatment.Contains(request))
                .ToList();

            return Ok(matchingInspections);
        }
    }

}
