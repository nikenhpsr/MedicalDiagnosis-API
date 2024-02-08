using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalDiagnosis_API.Data;
using MedicalDiagnosis_API.Models;

namespace MedicalDiagnosis_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Ensure this attribute is present to treat this controller as an API controller
    public class InspectionsController : ControllerBase
    {
        private readonly MedicalDiagnosisContext _context;

        public InspectionsController(MedicalDiagnosisContext context)
        {
            _context = context;
        }

        // GET: api/Inspections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalInspection>>> GetInspections()
        {
            return await _context.MedicalInspections
                                 .Include(m => m.Author)
                                 .Include(m => m.ParentInspection)
                                 .Include(m => m.Patient)
                                 .ToListAsync();
        }

        // GET: api/Inspections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalInspection>> GetInspection(string id)
        {
            var medicalInspection = await _context.MedicalInspections
                                                  .Include(m => m.Author)
                                                  .Include(m => m.ParentInspection)
                                                  .Include(m => m.Patient)
                                                  .FirstOrDefaultAsync(m => m.MedicalInspectionId == id);

            if (medicalInspection == null)
            {
                return NotFound();
            }

            return medicalInspection;
        }

        // POST: api/Inspections
        [HttpPost]
        public async Task<ActionResult<MedicalInspection>> PostInspection(MedicalInspection medicalInspection)
        {
            _context.MedicalInspections.Add(medicalInspection);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInspection), new { id = medicalInspection.MedicalInspectionId }, medicalInspection);
        }

        // PUT: api/Inspections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspection(string id, MedicalInspection medicalInspection)
        {
            if (id != medicalInspection.MedicalInspectionId)
            {
                return BadRequest();
            }

            _context.Entry(medicalInspection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalInspectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Inspections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspection(string id)
        {
            var medicalInspection = await _context.MedicalInspections.FindAsync(id);
            if (medicalInspection == null)
            {
                return NotFound();
            }

            _context.MedicalInspections.Remove(medicalInspection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicalInspectionExists(string id)
        {
            return _context.MedicalInspections.Any(e => e.MedicalInspectionId == id);
        }
    }
}
