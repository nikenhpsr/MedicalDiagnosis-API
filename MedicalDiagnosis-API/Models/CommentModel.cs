namespace MedicalDiagnosis_API.Models
{
    public class CommentModel
    {
        [Key]
        public Guid CommentId { get; set; } = Guid.NewGuid(); // Assuming CommentId is a UUID

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; } // Making ModifiedDate nullable

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; } // Added Content field with constraints

        // This is the foreign key property for Doctor
        public Guid? AuthorId { get; set; }

        // This is the navigation property to the Doctor class
        public DoctorModel? Author { get; set; }

        // This is the foreign key for the self-referencing relationship, using Guid for UUID
        public Guid? ParentCommentId { get; set; }

        // Navigation property for the self-referencing relationship
        public CommentModel? ParentComment { get; set; }

        // Collection navigation property for related child comments
        public List<CommentModel>? InnerComments { get; set; }

        public CommentModel()
        {
            // Initialize the list to prevent null reference exceptions
            InnerComments = new List<CommentModel>();
        }
    }
}
