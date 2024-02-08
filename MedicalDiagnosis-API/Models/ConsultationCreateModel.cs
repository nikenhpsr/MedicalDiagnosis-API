namespace MedicalDiagnosisAPI.Models
{
    public class ConsultationCreateModel
    {
        [Required]
        public Guid SpecialityId { get; set; }

        [Required]
        public InspectionCommentCreateModel Comment { get; set; }
    }

}