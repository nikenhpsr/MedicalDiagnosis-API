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

        //one-to-one relationship with InspectionModel
        public Guid InspectionId { get; set; }  // Foreign key property
        public virtual InspectionModel Inspection { get; set; }  // Navigation property

        // Collection navigation property for related Comments
        public virtual ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }
}
