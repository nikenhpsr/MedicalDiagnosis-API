namespace MedicalDiagnosis_API.Models
{
    using System;
    using System.Collections.Generic; // Required for List<>
    using System.ComponentModel.DataAnnotations;

    public class DoctorModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(1)]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        // Add this collection navigation property
        public virtual ICollection<CommentModel> Comments { get; set; }
        public virtual ICollection<InspectionModel> Inspections { get; set; }

        public DoctorModel()
        {
            // Initialize the Comments collection to prevent null reference exceptions
            Comments = new HashSet<CommentModel>();
            Inspections = new HashSet<InspectionModel>();
        }
    }


}
