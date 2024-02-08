namespace MedicalDiagnosis_API.Models
{
    public class Icd10RecordModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public string Code { get; set; }

        public string Name { get; set; }
    }

}
