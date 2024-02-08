namespace MedicalDiagnosis_API.Models
{
    public class Comment
    {
        [Key]
        public required string CommentId { get; set; }
        public DateTime CreateDateTime { get; set; }

        // This is the foreign key property for Doctor
        public string? CommentAuthorId { get; set; }

        // This is the navigation property to the Doctor class
        public Doctor? CommentAuthor { get; set; }

        // This is the foreign key for the self-referencing relationship
        public int? ParentCommentId { get; set; }

        // Navigation property for the self-referencing relationship
        public Comment? ParentComment { get; set; }

        // Collection navigation property for related child comments
        public List<Comment>? InnerComments { get; set; }

        public Comment()
        {
            // Initialize the list to prevent null reference exceptions
            InnerComments = new List<Comment>();
        }
    }
}
