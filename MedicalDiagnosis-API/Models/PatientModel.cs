namespace MedicalDiagnosis_API.Models
{
    public class PatientModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }


}
