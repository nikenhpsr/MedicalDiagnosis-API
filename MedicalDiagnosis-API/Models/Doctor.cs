namespace MedicalDiagnosis_API.Models
{
    public class Doctor
    {
        [Key]
        public required string DoctorId { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required Speciality Speciality { get; set; }
        public virtual ICollection<MedicalInspection> MedicalInspections { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Doctor()
        {
            // Initialize the collection to prevent null reference exceptions
            Comments = new HashSet<Comment>();
        }
    }
}
