namespace MedicalDiagnosisAPI.Models
{
    public class CommentCreateModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }

        public Guid? ParentId { get; set; }
    }

}