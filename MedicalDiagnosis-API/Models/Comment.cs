namespace MedicalDiagnosis_API.Models
{
    public class Comment
    {
        public DateTime CreateDateTime { get; set; }
        public Doctor? CommentAuthor { get; set; }
        public List<Comment>? InnerComments { get; set; }
    }
