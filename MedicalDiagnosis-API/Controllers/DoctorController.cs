using Microsoft.AspNetCore.Mvc;

namespace MedicalDiagnosis_API.Controllers
{
    [ApiController]
    [Route("api/doctor")]
    public class DoctorController : ControllerBase
    {
        // POST: api/doctor/register
        [HttpPost("register")]
        public IActionResult RegisterDoctor([FromBody] DoctorRegisterModel registerModel)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Here you would add logic to save the doctor's information in your database
            // For now, simulate the doctor being registered and a token being generated
            var token = GenerateToken(registerModel);

            return Ok(new { token });
        }

        // POST: api/doctor/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCredentialsModel loginModel)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Here you would add logic to validate the doctor's credentials
            // For now, simulate the login and token generation
            var token = GenerateToken(loginModel);

            return Ok(new { token });
        }

        // POST: api/doctor/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Here you would add logic to handle logout, such as invalidating the token
            return Ok(new { status = "Success", message = "Logged out successfully" });
        }

        // GET: api/doctor/profile
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            // Here you would retrieve the doctor's profile from the database
            // For now, return a simulated profile
            return Ok(new
            {
                id = Guid.NewGuid(),
                createTime = DateTime.UtcNow,
                name = "Doctor Name",
                birthday = DateTime.UtcNow.AddYears(-30), // Example birthday
                gender = "Male",
                email = "email@example.com",
                phone = "1234567890"
            });
        }

        // PUT: api/doctor/profile
        [HttpPut("profile")]
        public IActionResult EditProfile([FromBody] DoctorEditModel profileModel)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Here you would update the doctor's profile in the database
            // For now, simulate a successful profile update
            return Ok(new { status = "Success", message = "Profile updated successfully" });
        }

        private string GenerateToken(object model)
        {
            // This method should integrate with your authentication system to generate a token
            // For now, return a dummy token
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
