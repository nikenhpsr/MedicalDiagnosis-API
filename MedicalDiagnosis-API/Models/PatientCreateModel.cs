namespace MedicalDiagnosis_API.Models
{
    public class PatientCreateModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }

}
