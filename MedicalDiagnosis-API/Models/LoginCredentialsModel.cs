namespace MedicalDiagnosis_API.Models
{
    public class LoginCredentialsModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(1)]
        public string Password { get; set; }
    }

}
