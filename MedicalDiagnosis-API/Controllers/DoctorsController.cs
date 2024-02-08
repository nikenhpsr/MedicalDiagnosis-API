using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalDiagnosis_API.Data;
using MedicalDiagnosis_API.Models;

namespace MedicalDiagnosis_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly MedicalDiagnosisContext _context;

        public DoctorsController(MedicalDiagnosisContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorModel>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorModel>> GetDoctor(string id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<ActionResult<DoctorModel>> PostDoctor(DoctorModel doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.DoctorId }, doctor);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(string id, DoctorModel doctor)
        {
            if (id != doctor.DoctorId)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(string id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
