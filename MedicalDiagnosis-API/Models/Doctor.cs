namespace MedicalDiagnosis_API.Models
{
    public class Doctor
    {
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required Speciality Speciality { get; set; }
    }
}
