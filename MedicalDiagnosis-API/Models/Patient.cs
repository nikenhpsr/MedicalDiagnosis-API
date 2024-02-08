namespace MedicalDiagnosis_API.Models
{
    public class Patient
    {
        [Key]
        public string PatientId { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public virtual ICollection<MedicalInspection> MedicalInspections { get; set; }
    }

}
