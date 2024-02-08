namespace MedicalDiagnosis_API.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DiagnosisCreateModel
    {
        [Required]
        public Guid IcdDiagnosisId { get; set; } // Assuming icdDiagnosisId is a UUID, using Guid in C#

        [StringLength(5000)]
        public string Description { get; set; } // Nullable and with a maximum length of 5000 characters

        [Required]
        public DiagnosisType Type { get; set; } // Using the DiagnosisType enum
    }

}
