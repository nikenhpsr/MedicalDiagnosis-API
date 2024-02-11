namespace MedicalDiagnosis_API.Models
{
    public class InspectionConsultationModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid InspectionId { get; set; }
        public InspectionModel Inspection { get; set; }

        [Required]
        public SpecialityModel Speciality { get; set; } // Assuming SpecialityModel is defined

        [Required]
        public InspectionCommentModel RootComment { get; set; }

        public int CommentsNumber { get; set; }

        public Guid ConsultationId { get; set; }
        public ConsultationModel Consultation { get; set; }
    }

}
