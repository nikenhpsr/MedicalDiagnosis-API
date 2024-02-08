namespace MedicalDiagnosis_API.Models
{
    public class DoctorRegisterModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(1)]
        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public Guid SpecialityId { get; set; } // Assuming speciality is a UUID
    }

}
