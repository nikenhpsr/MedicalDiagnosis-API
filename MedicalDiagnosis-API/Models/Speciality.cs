namespace MedicalDiagnosis_API.Models
{
    public class Speciality
    {
        [Key]
        public string SpecialityId { get; set; }
        public required string Name { get; set; }
    }
}
