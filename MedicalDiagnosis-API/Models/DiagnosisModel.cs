namespace MedicalDiagnosis_API.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DiagnosisModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        [Required]
        [MinLength(1)]
        public string Code { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid InspectionModelId { get; set; } // Foreign key property
        public InspectionModel Inspection { get; set; } // Navigation property


        [Required]
        public DiagnosisType Type { get; set; }
    }

}
