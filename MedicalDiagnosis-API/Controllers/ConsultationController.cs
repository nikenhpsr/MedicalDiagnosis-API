using MedicalDiagnosis_API.Data;
using Microsoft.AspNetCore.Mvc;

namespace MedicalDiagnosis_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultationController : ControllerBase
    {
        private readonly MedicalDiagnosisContext _context; // Your database context

        public ConsultationController(MedicalDiagnosisContext context)
        {
            _context = context;
        }

        // GET: api/consultation
        [HttpGet]
        public async Task<IActionResult> GetConsultations([FromQuery] bool grouped = false, [FromQuery] List<string> icdRoots = null, [FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var consultationsQuery = _context.Consultations.AsQueryable();

            // Implement grouping logic if required
            if (grouped)
            {
                // Group consultations based on your grouping criteria, possibly involving chains of consultations
            }

            var totalConsultations = await consultationsQuery.CountAsync();
            var consultations = await consultationsQuery.Skip((page - 1) * size).Take(size).ToListAsync();

            return Ok(new
            {
                inspections = consultations, // Replace stub with actual data
                pagination = new { size, count = totalConsultations, current = page }
            });
        }

        // GET: api/consultation/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultationById([FromRoute] Guid id)
        {
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
            {
                return NotFound();
            }

            return Ok(consultation);
        }

        // POST: api/consultation/{id}/comment
        [HttpPost("{id}/comment")]
        public async Task<IActionResult> AddCommentToConsultation([FromRoute] Guid id, [FromBody] CommentModel comment)
        {
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            var newComment = new CommentModel
            {
                Content = comment.Content,
                CreateTime = DateTime.UtcNow
                // Set other properties as necessary
            };

            consultation.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            return Ok(newComment.CommentId);
        }

        // PUT: api/consultation/comment/{id}
        [HttpPut("comment/{id}")]
        public async Task<IActionResult> EditComment([FromRoute] Guid id, [FromBody] CommentModel comment)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.Content = comment.Content;
            // Update other properties as necessary

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
