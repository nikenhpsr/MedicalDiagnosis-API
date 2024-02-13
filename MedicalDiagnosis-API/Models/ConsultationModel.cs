namespace MedicalDiagnosis_API.Models
{
    public class ConsultationModel
    {
        [Key]
        public Guid ConsultationId { get; set; } = Guid.NewGuid(); // Assuming ConsultationId is a UUID, using Guid in C#

        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;

        // Foreign key for Speciality
        public Guid SpecialityId { get; set; } // Assuming SpecialityId is a UUID, using Guid in C#

        // Navigation property for Speciality
        public virtual SpecialityModel Speciality { get; set; }

        // Collection navigation property for related Comments
        public virtual ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
        public virtual ICollection<InspectionModel> Inspections { get; set; } = new List<InspectionModel>();
    }
}
