namespace MedicalDiagnosis_API.Models
{
    public class Patient
    {
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }

}
