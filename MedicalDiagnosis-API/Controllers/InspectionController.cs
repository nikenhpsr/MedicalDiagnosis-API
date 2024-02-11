using Microsoft.AspNetCore.Mvc;

namespace MedicalDiagnosis_API.Controllers
{
    [ApiController]
    [Route("api/inspection")]
    public class InspectionController : ControllerBase
    {
        private readonly MedicalDiagnosisContext _context;

        public InspectionController(MedicalDiagnosisContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetInspectionById([FromRoute] Guid id)
        {
            var inspection = _context.Inspections
                .Include(i => i.BaseInspectionId)
                .Include(i => i.PreviousInspectionId)
                .FirstOrDefault(i => i.Id == id);

            if (inspection == null)
            {
                return NotFound();
            }

            return Ok(inspection);
        }

        [HttpPut("{id}")]
        public IActionResult EditInspection([FromRoute] Guid id, [FromBody] InspectionEditModel model)
        {
            var inspection = _context.Inspections.Find(id);

            if (inspection == null)
            {
                return NotFound();
            }

            // Update properties
            inspection.Anamnesis = model.Anamnesis;
            inspection.Complaints = model.Complaints;
            inspection.Treatment = model.Treatment;
            inspection.Conclusion = model.Conclusion;
            inspection.NextVisitDate = model.NextVisitDate;
            inspection.DeathDate = model.DeathDate;

            _context.SaveChanges();

            return Ok(new { status = "Success", message = "Inspection updated successfully" });
        }

        [HttpGet("{id}/chain")]
        public IActionResult GetInspectionChain([FromRoute] Guid id)
        {
            var rootInspection = _context.Inspections.Find(id);

            if (rootInspection == null)
            {
                return NotFound();
            }

            var chain = _context.Inspections
                .Where(i => i.BaseInspectionId == id || i.Id == id)
                .OrderBy(i => i.Date)
                .ToList();

            return Ok(chain);
        }
    }
}
