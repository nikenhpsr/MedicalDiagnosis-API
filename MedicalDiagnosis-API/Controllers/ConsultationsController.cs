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
    [ApiController]
    public class ConsultationsController : ControllerBase
    {
        private readonly MedicalDiagnosisContext _context;

        public ConsultationsController(MedicalDiagnosisContext context)
        {
            _context = context;
        }

        // GET: api/Consultations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consultation>>> GetConsultations()
        {
            return await _context.Consultations.Include(c => c.MedicalInspection).ToListAsync();
        }

        // GET: api/Consultations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consultation>> GetConsultation(string id)
        {
            var consultation = await _context.Consultations.Include(c => c.MedicalInspection).FirstOrDefaultAsync(m => m.ConsultationId == id);

            if (consultation == null)
            {
                return NotFound();
            }

            return consultation;
        }

        // POST: api/Consultations
        [HttpPost]
        public async Task<ActionResult<Consultation>> PostConsultation(Consultation consultation)
        {
            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsultation", new { id = consultation.ConsultationId }, consultation);
        }

        // PUT: api/Consultations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsultation(string id, Consultation consultation)
        {
            if (id != consultation.ConsultationId)
            {
                return BadRequest();
            }

            _context.Entry(consultation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultationExists(id))
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

        // DELETE: api/Consultations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultation(string id)
        {
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            _context.Consultations.Remove(consultation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultationExists(string id)
        {
            return _context.Consultations.Any(e => e.ConsultationId == id);
        }
    }
}
