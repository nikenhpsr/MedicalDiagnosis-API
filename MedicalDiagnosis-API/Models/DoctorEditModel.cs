namespace MedicalDiagnosis_API.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DoctorEditModel
    {
        [Required]
        [EmailAddress]
        [MinLength(1)]
        public string Email { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Phone]
        public string Phone { get; set; }
    }

}
