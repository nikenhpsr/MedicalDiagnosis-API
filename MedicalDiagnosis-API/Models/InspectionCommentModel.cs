namespace MedicalDiagnosis_API.Models
{
    public class InspectionCommentModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public Guid? ParentId { get; set; } // Nullable for optional parent comment

        public string Content { get; set; } // Nullable for optional content

        [Required]
        public DoctorModel Author { get; set; } // Embedded DoctorModel

        public DateTime? ModifyTime { get; set; } // Nullable for optional modification time
    }

}
