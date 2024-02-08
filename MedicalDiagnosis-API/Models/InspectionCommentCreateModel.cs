namespace MedicalDiagnosis_API.Models
{
    using System.ComponentModel.DataAnnotations;

    public class InspectionCommentCreateModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }
    }

}
