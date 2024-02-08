namespace MedicalDiagnosis_API.Models
{
    public class InspectionConsultationModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid InspectionId { get; set; } // Assuming inspectionId is a UUID

        [Required]
        public SpecialityModel Speciality { get; set; } // Assuming SpecialityModel is defined

        [Required]
        public InspectionCommentModel RootComment { get; set; }

        public int CommentsNumber { get; set; }
    }

}
