using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalDiagnosis_API.Data;
using MedicalDiagnosis_API.Models;

namespace MedicalDiagnosis_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly MedicalDiagnosisContext _context;

        public PatientsController(MedicalDiagnosisContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientModel>>> GetPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientModel>> GetPatient(string id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<PatientModel>> CreatePatient(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(string id, PatientModel patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PatientExists(string id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }
    }
}
